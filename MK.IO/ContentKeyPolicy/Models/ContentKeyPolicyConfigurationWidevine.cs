﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationWidevine : ContentKeyPolicyConfiguration
    {
        public ContentKeyPolicyConfigurationWidevine(string widevineTemplate)
        {
            WidevineTemplate = widevineTemplate;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyWidevineConfiguration";

        [JsonProperty("widevineTemplate")]
        public string WidevineTemplate { get; set; }
    }
}