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

        [JsonProperty("gracePeriod")]
        public string GracePeriod { get; set; }

        [JsonProperty("relativeBeginDate")]
        public string RelativeBeginDate { get; set; }

        [JsonProperty("relativeExpirationDate")]
        public string RelativeExpirationDate { get; set; }
    }

    public class PlayReadyPlayRight
    {
        [JsonProperty("scmsRestriction")]
        public int ScmsRestriction { get; set; }
       

        [JsonProperty("allowPassingVideoContentToUnknownOutput")]
        public string AllowPassingVideoContentToUnknownOutput { get; set; }

        [JsonProperty("analogVideoOpl")]
        public int AnalogVideoOpl { get; set; }

        [JsonProperty("compressedDigitalAudioOpl")]
        public int CompressedDigitalAudioOpl { get; set; }

        [JsonProperty("compressedDigitalVideoOpl")]
        public int CompressedDigitalVideoOpl { get; set; }

        [JsonProperty("uncompressedDigitalAudioOpl")]
        public int UncompressedDigitalAudioOpl { get; set; }

        [JsonProperty("uncompressedDigitalVideoOpl")]
        public int UncompressedDigitalVideoOpl { get; set; }

        [JsonProperty("digitalVideoOnlyContentRestriction")]
        public bool DigitalVideoOnlyContentRestriction { get; set; }
         
        [JsonProperty("imageConstraintForAnalogComponentVideoRestriction")]
        public bool ImageConstraintForAnalogComponentVideoRestriction { get; set; }

        [JsonProperty("imageConstraintForAnalogComputerMonitorRestriction")]
        public bool ImageConstraintForAnalogComputerMonitorRestriction { get; set; }

        [JsonProperty("firstPlayExpiration")]
        public string FirstPlayExpiration { get; set; }
    }
}