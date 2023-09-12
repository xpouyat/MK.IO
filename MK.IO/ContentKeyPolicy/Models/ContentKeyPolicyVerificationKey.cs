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
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicySymmetricTokenKey), "#Microsoft.Media.ContentKeyPolicySymmetricTokenKey")]
   
    public abstract class ContentKeyPolicyVerificationKey
    {
        [JsonProperty("@odata.type")]
        public virtual string OdataType { get; set; }

        [JsonProperty("keyValue")]
        public string KeyValue { get; set; }
    }

}
