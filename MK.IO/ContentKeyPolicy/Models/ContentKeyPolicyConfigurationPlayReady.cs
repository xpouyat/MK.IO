// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationPlayReady : ContentKeyPolicyConfigurationBase
    {
       

      

        [JsonProperty("licenses")]
        public List<ContentKeyPolicyPlayReadyLicense> Licenses { get; set; }
    }
}