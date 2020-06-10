// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.Network.Models
{
    /// <summary> The routes table associated with the ExpressRouteCircuit. </summary>
    public partial class ExpressRouteCircuitRoutesTableSummary
    {
        /// <summary> Initializes a new instance of ExpressRouteCircuitRoutesTableSummary. </summary>
        internal ExpressRouteCircuitRoutesTableSummary()
        {
        }

        /// <summary> Initializes a new instance of ExpressRouteCircuitRoutesTableSummary. </summary>
        /// <param name="neighbor"> IP address of the neighbor. </param>
        /// <param name="v"> BGP version number spoken to the neighbor. </param>
        /// <param name="as"> Autonomous system number. </param>
        /// <param name="upDown"> The length of time that the BGP session has been in the Established state, or the current status if not in the Established state. </param>
        /// <param name="statePfxRcd"> Current state of the BGP session, and the number of prefixes that have been received from a neighbor or peer group. </param>
        internal ExpressRouteCircuitRoutesTableSummary(string neighbor, int? v, int? @as, string upDown, string statePfxRcd)
        {
            Neighbor = neighbor;
            V = v;
            As = @as;
            UpDown = upDown;
            StatePfxRcd = statePfxRcd;
        }

        /// <summary> IP address of the neighbor. </summary>
        public string Neighbor { get; }
        /// <summary> BGP version number spoken to the neighbor. </summary>
        public int? V { get; }
        /// <summary> Autonomous system number. </summary>
        public int? As { get; }
        /// <summary> The length of time that the BGP session has been in the Established state, or the current status if not in the Established state. </summary>
        public string UpDown { get; }
        /// <summary> Current state of the BGP session, and the number of prefixes that have been received from a neighbor or peer group. </summary>
        public string StatePfxRcd { get; }
    }
}