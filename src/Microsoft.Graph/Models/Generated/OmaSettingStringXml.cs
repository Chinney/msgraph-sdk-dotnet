// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: Templates\CSharp\Model\ComplexType.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type OmaSettingStringXml.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class OmaSettingStringXml : OmaSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OmaSettingStringXml"/> class.
        /// </summary>
        public OmaSettingStringXml()
        {
            this.ODataType = "microsoft.graph.omaSettingStringXml";
        }

        /// <summary>
        /// Gets or sets fileName.
        /// File name associated with the Value property (.xml).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fileName", Required = Newtonsoft.Json.Required.Default)]
        public string FileName { get; set; }
    
        /// <summary>
        /// Gets or sets value.
        /// Value. (UTF8 encoded byte array)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value", Required = Newtonsoft.Json.Required.Default)]
        public byte[] Value { get; set; }
    
    }
}
