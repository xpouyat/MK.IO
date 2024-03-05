using Newtonsoft.Json;

namespace MK.IO
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
