using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.IO
{
    public class ContentKeyPolicyTokenRestriction : ContentKeyPolicyRestriction
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("restrictionTokenType")]
        public string RestrictionTokenType { get; set; }

        [JsonProperty("primaryVerificationKey")]
        public ContentKeyPolicyVerificationKey PrimaryVerificationKey { get; set; }

    }

   
}
