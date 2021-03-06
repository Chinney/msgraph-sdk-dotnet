// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: Templates\CSharp\Requests\EntityCollectionRequestBuilder.cs.tt
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type DeviceConfigurationUserStatusesCollectionRequestBuilder.
    /// </summary>
    public partial class DeviceConfigurationUserStatusesCollectionRequestBuilder : BaseRequestBuilder, IDeviceConfigurationUserStatusesCollectionRequestBuilder
    {
        /// <summary>
        /// Constructs a new DeviceConfigurationUserStatusesCollectionRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public DeviceConfigurationUserStatusesCollectionRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IDeviceConfigurationUserStatusesCollectionRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IDeviceConfigurationUserStatusesCollectionRequest Request(IEnumerable<Option> options)
        {
            return new DeviceConfigurationUserStatusesCollectionRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="IDeviceConfigurationUserStatusRequestBuilder"/> for the specified DeviceConfigurationDeviceConfigurationUserStatus.
        /// </summary>
        /// <param name="id">The ID for the DeviceConfigurationDeviceConfigurationUserStatus.</param>
        /// <returns>The <see cref="IDeviceConfigurationUserStatusRequestBuilder"/>.</returns>
        public IDeviceConfigurationUserStatusRequestBuilder this[string id]
        {
            get
            {
                return new DeviceConfigurationUserStatusRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
    }
}
