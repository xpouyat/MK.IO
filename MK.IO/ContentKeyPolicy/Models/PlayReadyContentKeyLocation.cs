using JsonSubTypes;
using Newtonsoft.Json;

namespace MK.IO
{
    [JsonConverter(typeof(JsonSubtypes), "@odata.type")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader), "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader")]
    //
    // Summary:
    //     Base class for PlayReady content key location. A derived class must be used
    //     to specify a location.
    public class PlayReadyContentKeyLocation
    {
        [JsonProperty("@odata.type")]
        public virtual string OdataType { get; set; }
    }
}
