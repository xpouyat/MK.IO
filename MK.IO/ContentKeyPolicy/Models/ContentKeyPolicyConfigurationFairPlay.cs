// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyConfigurationFairPlay : ContentKeyPolicyConfigurationBase
    {

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("fairPlayPfx")]
        public string FairPlayPfx { get; set; }

        [JsonProperty("rentalDuration")]
        public int RentalDuration { get; set; }

        [JsonProperty("fairPlayPfxPassword")]
        public string FairPlayPfxPassword { get; set; }

        [JsonProperty("rentalAndLeaseKeyType")]
        public string RentalAndLeaseKeyType { get; set; }

    }
}