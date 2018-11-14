// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Model\EnumType.cs.tt


namespace Microsoft.Graph
{
    using Newtonsoft.Json;

    /// <summary>
    /// The enum UserIdentityType.
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    public enum UserIdentityType
    {
    
        /// <summary>
        /// aad User
        /// </summary>
        AadUser = 0,
	
        /// <summary>
        /// on Premise Aad User
        /// </summary>
        OnPremiseAadUser = 1,
	
        /// <summary>
        /// anonymous Guest
        /// </summary>
        AnonymousGuest = 2,
	
        /// <summary>
        /// federated User
        /// </summary>
        FederatedUser = 3,
	
    }
}