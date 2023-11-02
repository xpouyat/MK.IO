// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationPlayReady : ContentKeyPolicyConfiguration
    {
        public ContentKeyPolicyConfigurationPlayReady(List<ContentKeyPolicyPlayReadyLicense> licenses)
        {
            Licenses = licenses;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyPlayReadyConfiguration";

        [JsonProperty("licenses")]
        public List<ContentKeyPolicyPlayReadyLicense> Licenses { get; set; }
    }
}