// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{

    public class ContentKeyPolicyPlayReadyLicense
    {
        public ContentKeyPolicyPlayReadyLicense(string contentType, string licenseType, string playReadySecurityLevel, bool allowTestDevices, ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader contentKeyLocation, PlayReadyPlayRight playRight)
        {
            ContentType = contentType;
            LicenseType = licenseType;
            PlayReadySecurityLevel = playReadySecurityLevel;
            AllowTestDevices = allowTestDevices;
            ContentKeyLocation = contentKeyLocation;
            PlayRight = playRight;
        }


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
        public PlayReadyContentKeyLocation ContentKeyLocation { get; set; }

    }

    public class PlayReadyPlayRight
    {
        public PlayReadyPlayRight(int firstPlayExpiration, int licenseValidityDuration, int playEnablers, int scmsRestriction, string allowPassingVideoContentToUnknownOutput)
        {
            ScmsRestriction = scmsRestriction;
            AllowPassingVideoContentToUnknownOutput = allowPassingVideoContentToUnknownOutput;
        }

        [JsonProperty("scmsRestriction")]
        public int ScmsRestriction { get; set; }

        [JsonProperty("allowPassingVideoContentToUnknownOutput")]
        public string AllowPassingVideoContentToUnknownOutput { get; set; }
    }
 


}