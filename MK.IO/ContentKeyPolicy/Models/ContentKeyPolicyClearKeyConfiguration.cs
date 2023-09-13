// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyClearKeyConfiguration : ContentKeyPolicyConfiguration
    {
        public ContentKeyPolicyClearKeyConfiguration()
        {
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyClearKeyConfiguration";
    }
}