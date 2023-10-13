// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationFairPlay : ContentKeyPolicyConfiguration
    {

        public ContentKeyPolicyConfigurationFairPlay(string ask, string fairPlayPfx, string fairPlayPfxPassword, long rentalDuration, string rentalAndLeaseKeyType)
        {
            Ask = ask;
            FairPlayPfx = fairPlayPfx;
            RentalDuration = rentalDuration;
            FairPlayPfxPassword = fairPlayPfxPassword;
            RentalAndLeaseKeyType = rentalAndLeaseKeyType;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyConfigurationFairPlay";

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("fairPlayPfx")]
        public string FairPlayPfx { get; set; }

        [JsonProperty("rentalDuration")]
        public long RentalDuration { get; set; }

        [JsonProperty("fairPlayPfxPassword")]
        public string FairPlayPfxPassword { get; set; }

        [JsonProperty("rentalAndLeaseKeyType")]
        public string RentalAndLeaseKeyType { get; set; }
    }
}