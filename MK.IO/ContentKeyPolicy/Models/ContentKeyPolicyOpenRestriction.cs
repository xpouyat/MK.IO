using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyOpenRestriction : ContentKeyPolicyRestriction
    {
        public ContentKeyPolicyOpenRestriction()
        {
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.ContentKeyPolicyOpenRestriction";
    }
}
