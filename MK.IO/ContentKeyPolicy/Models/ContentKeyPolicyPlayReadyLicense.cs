// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyPlayReadyLicense
    {
        [JsonProperty("playRight")]
        public PlayReadyPlayRight PlayRight { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("licenseType")]
        public string LicenseType { get; set; }

        [JsonProperty("securityLevel")]
        public string PlayReadySecurityLevel { get; set; }

        [JsonProperty("allowTestDevices")]
        public bool AllowTestDevices { get; set; }

        [JsonProperty("contentKeyLocation")]
        public ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader ContentKeyLocation { get; set; }

    }

    public class PlayReadyPlayRight
    {
        [JsonProperty("scmsRestriction")]
        public int ScmsRestriction { get; set; }

        [JsonProperty("allowPassingVideoContentToUnknownOutput")]
        public string AllowPassingVideoContentToUnknownOutput { get; set; }
    }

    


}