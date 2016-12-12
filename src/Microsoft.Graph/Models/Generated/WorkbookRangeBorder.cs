// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.


namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type Workbook Range Border.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class WorkbookRangeBorder : Entity
    {
    
        /// <summary>
        /// Gets or sets color.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "color", Required = Required.Default)]
        public string Color { get; set; }
    
        /// <summary>
        /// Gets or sets side index.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sideIndex", Required = Required.Default)]
        public string SideIndex { get; set; }
    
        /// <summary>
        /// Gets or sets style.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "style", Required = Required.Default)]
        public string Style { get; set; }
    
        /// <summary>
        /// Gets or sets weight.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "weight", Required = Required.Default)]
        public string Weight { get; set; }
    
    }
}
