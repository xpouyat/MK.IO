using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyOpenRestriction : ContentKeyPolicyRestriction
    {
        public ContentKeyPolicyOpenRestriction()
        {
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyOpenRestriction";
    }
}
