// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityRequestBuilder.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The type NetworkIPv4ConfigurationManagementConditionRequestBuilder.
    /// </summary>
    public partial class NetworkIPv4ConfigurationManagementConditionRequestBuilder : NetworkManagementConditionRequestBuilder, INetworkIPv4ConfigurationManagementConditionRequestBuilder
    {

        /// <summary>
        /// Constructs a new NetworkIPv4ConfigurationManagementConditionRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public NetworkIPv4ConfigurationManagementConditionRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public new INetworkIPv4ConfigurationManagementConditionRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public new INetworkIPv4ConfigurationManagementConditionRequest Request(IEnumerable<Option> options)
        {
            return new NetworkIPv4ConfigurationManagementConditionRequest(this.RequestUrl, this.Client, options);
        }
    
    }
}