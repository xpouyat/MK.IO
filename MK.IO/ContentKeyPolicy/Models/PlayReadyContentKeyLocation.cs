using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
