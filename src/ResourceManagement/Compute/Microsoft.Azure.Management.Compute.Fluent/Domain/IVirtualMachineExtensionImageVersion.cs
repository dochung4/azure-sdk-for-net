// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
namespace Microsoft.Azure.Management.Compute.Fluent
{
    using Models;
    using Microsoft.Azure.Management.Resource.Fluent.Core;

    /// <summary>
    /// An immutable client-side representation of an Azure virtual machine extension image version.
    /// </summary>
    public interface IVirtualMachineExtensionImageVersion  :
        IHasInner<Models.VirtualMachineExtensionImageInner>,
        IHasName
    {
        /// <summary>
        /// Gets the region in which virtual machine extension image version is available.
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// Gets the resource ID of the extension image version.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the virtual machine extension image type this version belongs to.
        /// </summary>
        Microsoft.Azure.Management.Compute.Fluent.IVirtualMachineExtensionImageType Type { get; }

        /// <return>Virtual machine extension image this version represents.</return>
        Microsoft.Azure.Management.Compute.Fluent.IVirtualMachineExtensionImage GetImage();
    }
}