using JsonSubTypes;
using Newtonsoft.Json;

namespace MK.IO
{

    [JsonConverter(typeof(JsonSubtypes), "@odata.type")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicySymmetricTokenKey), "#Microsoft.Media.ContentKeyPolicySymmetricTokenKey")]

    //
    // Summary:
    //     Base class for Content Key Policy verification key. A derived class must be used
    //     to specify the key.
    public abstract class ContentKeyPolicyVerificationKey
    {
        [JsonProperty("@odata.type")]
        internal virtual string OdataType { get; set; }
    }
}
