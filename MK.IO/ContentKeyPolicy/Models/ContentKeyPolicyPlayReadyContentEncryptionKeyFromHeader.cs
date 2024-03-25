// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{
    /// <summary>
    /// Specifies that the content key ID is in the PlayReady header.
    /// </summary>
    public class ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader : PlayReadyContentKeyLocation
    {
        public ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader()
        {
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader";
    }
}
