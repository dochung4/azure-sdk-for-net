﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Tests.Common;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.Network.Fluent.Models;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Azure.Tests.Network.LoadBalancer
{
    /// <summary>
    /// Internet-facing LB test with NAT pool test. 
    /// </summary>
    public class InternetWithNatPool : TestTemplate<ILoadBalancer, ILoadBalancers, INetworkManager>
    {
        private IPublicIpAddresses pips;
        private IVirtualMachines vms;
        private IAvailabilitySets availabilitySets;
        private INetworks networks;
        private LoadBalancerHelper loadBalancerHelper;

        public InternetWithNatPool(
                IPublicIpAddresses pips,
                IVirtualMachines vms,
                INetworks networks,
                IAvailabilitySets availabilitySets,
                [CallerMemberName] string methodName = "testframework_failed")
            : base(methodName)
        {
            loadBalancerHelper = new LoadBalancerHelper(TestUtilities.GenerateName(methodName));

            this.pips = pips;
            this.vms = vms;
            this.availabilitySets = availabilitySets;
            this.networks = networks;
        }

        public override void Print(ILoadBalancer resource)
        {
            LoadBalancerHelper.PrintLB(resource);
        }

        public override ILoadBalancer CreateResource(ILoadBalancers resources)
        {
            var existingVMs = loadBalancerHelper.EnsureVMs(this.networks, this.vms, this.availabilitySets, 2);
            var existingPips = loadBalancerHelper.EnsurePIPs(pips);

            // Create a load balancer
            var lb = resources.Define(loadBalancerHelper.LoadBalancerName)
                        .WithRegion(loadBalancerHelper.Region)
                        .WithExistingResourceGroup(loadBalancerHelper.GroupName)

                        // Frontends
                        .WithExistingPublicIpAddress(existingPips.ElementAt(0))
                        .DefinePublicFrontend("frontend1")
                            .WithExistingPublicIpAddress(existingPips.ElementAt(1))
                            .Attach()

                        // Backends
                        .WithExistingVirtualMachines(existingVMs.ToArray())
                        .DefineBackend("backend1")
                            .Attach()

                        // Probes
                        .DefineTcpProbe("tcpProbe1")
                            .WithPort(25)               // Required
                            .WithIntervalInSeconds(15)  // Optionals
                            .WithNumberOfProbes(5)
                            .Attach()
                        .DefineHttpProbe("httpProbe1")
                            .WithRequestPath("/")       // Required
                            .WithIntervalInSeconds(13)  // Optionals
                            .WithNumberOfProbes(4)
                            .Attach()

                        // Load balancing rules
                        .DefineLoadBalancingRule("rule1")
                            .WithProtocol(TransportProtocol.Tcp)    // Required
                            .WithFrontend("frontend1")
                            .WithFrontendPort(81)
                            .WithProbe("tcpProbe1")
                            .WithBackend("backend1")
                            .WithBackendPort(82)                    // Optionals
                            .WithIdleTimeoutInMinutes(10)
                            .WithLoadDistribution(LoadDistribution.SourceIP)
                            .Attach()

                        // Inbound NAT pools
                        .DefineInboundNatPool("natpool1")
                            .WithProtocol(TransportProtocol.Tcp)
                            .WithFrontend("frontend1")
                            .WithFrontendPortRange(2000, 2001)
                            .WithBackendPort(8080)
                            .Attach()

                        .Create();

            // Verify frontends
            Assert.True(lb.Frontends.ContainsKey("frontend1"));
            Assert.True(lb.Frontends.ContainsKey("default"));
            Assert.Equal(lb.Frontends.Count, 2);

            // Verify backends
            Assert.True(lb.Backends.ContainsKey("default"));
            Assert.True(lb.Backends.ContainsKey("backend1"));
            Assert.Equal(lb.Backends.Count, 2);

            // Verify probes
            Assert.True(lb.HttpProbes.ContainsKey("httpProbe1"));
            Assert.True(lb.TcpProbes.ContainsKey("tcpProbe1"));
            Assert.True(!lb.HttpProbes.ContainsKey("default"));
            Assert.True(!lb.TcpProbes.ContainsKey("default"));

            // Verify rules
            Assert.True(lb.LoadBalancingRules.ContainsKey("rule1"));
            Assert.True(!lb.LoadBalancingRules.ContainsKey("default"));
            Assert.Equal(lb.LoadBalancingRules.Values.Count(), 1);
            var rule = lb.LoadBalancingRules["rule1"];
            Assert.True(rule.Backend.Name.Equals("backend1", StringComparison.OrdinalIgnoreCase));
            Assert.True(rule.Frontend.Name.Equals("frontend1", StringComparison.OrdinalIgnoreCase));
            Assert.True(rule.Probe.Name.Equals("tcpProbe1", StringComparison.OrdinalIgnoreCase));
            Assert.Equal(TransportProtocol.Tcp, rule.Protocol);

            // Verify inbound NAT pools
            Assert.True(lb.InboundNatPools.ContainsKey("natpool1"));
            Assert.Equal(lb.InboundNatPools.Count, 1);
            var inboundNatPool = lb.InboundNatPools["natpool1"];
            Assert.True(inboundNatPool.Frontend.Name.Equals("frontend1"));
            Assert.Equal(inboundNatPool.FrontendPortRangeStart, 2000);
            Assert.Equal(inboundNatPool.FrontendPortRangeEnd, 2001);
            Assert.Equal(inboundNatPool.BackendPort, 8080);

            return lb;
        }
        
        public override ILoadBalancer UpdateResource(ILoadBalancer resource)
        {
            resource = resource.Update()
                        .WithoutFrontend("default")
                        .WithoutBackend("default")
                        .WithoutBackend("backend1")
                        .WithoutLoadBalancingRule("rule1")
                        .WithoutInboundNatPool("natpool1")
                        .WithoutProbe("tcpProbe1")
                        .WithoutProbe("httpProbe1")
                        .WithTag("tag1", "value1")
                        .WithTag("tag2", "value2")
                        .Apply();

            resource.Refresh();
            Assert.True(resource.Tags.ContainsKey("tag1"));

            // Verify frontends
            Assert.False(resource.Frontends.ContainsKey("default"));
            Assert.Equal(1, resource.Frontends.Count);

            // Verify probes
            Assert.False(resource.HttpProbes.ContainsKey("httpProbe1"));
            Assert.False(resource.HttpProbes.ContainsKey("tcpProbe1"));
            Assert.Equal(0, resource.HttpProbes.Count);
            Assert.Equal(0, resource.TcpProbes.Count);

            // Verify backends
            Assert.False(resource.Backends.ContainsKey("default"));
            Assert.False(resource.Backends.ContainsKey("backend1"));
            Assert.Equal(0, resource.Backends.Count);

            // Verify rules
            Assert.False(resource.LoadBalancingRules.ContainsKey("rule1"));
            Assert.Equal(0, resource.LoadBalancingRules.Count);

            // Verify NAT pools
            Assert.False(resource.InboundNatPools.ContainsKey("natpool1"));

            return resource;
        }
    }
}
