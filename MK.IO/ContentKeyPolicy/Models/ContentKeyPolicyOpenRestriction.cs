using Newtonsoft.Json;

namespace MK.IO
{
    /// <summary>
    /// Represents an open restriction. License or key will be delivered on every request.
    /// </summary>
    public class ContentKeyPolicyOpenRestriction : ContentKeyPolicyRestriction
    {
        public ContentKeyPolicyOpenRestriction()
        {
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.ContentKeyPolicyOpenRestriction";
    }
}
