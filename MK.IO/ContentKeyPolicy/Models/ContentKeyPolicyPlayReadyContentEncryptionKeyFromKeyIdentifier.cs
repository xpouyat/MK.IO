using Newtonsoft.Json;

namespace MK.IO
{
    /// <summary>
    /// Specifies that the content key ID is specified in the PlayReady configuration.
    /// </summary>
    public class ContentKeyPolicyPlayReadyContentEncryptionKeyFromKeyIdentifier : PlayReadyContentKeyLocation
    {
        public ContentKeyPolicyPlayReadyContentEncryptionKeyFromKeyIdentifier()
        {
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromKeyIdentifier";
    }
}
