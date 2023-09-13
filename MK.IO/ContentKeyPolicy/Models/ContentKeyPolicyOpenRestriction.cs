using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
