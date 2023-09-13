using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.IO
{
    public class ContentKeyPolicySymmetricTokenKey : ContentKeyPolicyVerificationKey
    {
        public ContentKeyPolicySymmetricTokenKey(string keyValue)
        {
            KeyValue = keyValue;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicySymmetricTokenKey";
        
        [JsonProperty("keyValue")]
        public string KeyValue { get; set; }
    }
}
