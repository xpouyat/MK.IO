using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicySymmetricTokenKey : ContentKeyPolicyVerificationKey
    {
        public ContentKeyPolicySymmetricTokenKey(string keyValue)
        {
            KeyValue = keyValue;
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.ContentKeyPolicySymmetricTokenKey";

        [JsonProperty("keyValue")]
        public string KeyValue { get; set; }
    }
}
