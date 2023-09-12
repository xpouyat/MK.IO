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
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyTokenRestriction), "#Microsoft.Media.ContentKeyPolicyTokenRestriction")]
    [JsonSubtypes.KnownSubType(typeof(ContentKeyPolicyOpenRestriction), "#Microsoft.Media.ContentKeyPolicyOpenRestriction")]

    public class ContentKeyPolicyRestrictionBase
    {

        [JsonProperty("@odata.type")]
        public virtual string OdataType { get; set; }
    }
}
