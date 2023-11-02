using Newtonsoft.Json;

namespace MK.IO
{
    public class ContentKeyPolicyTokenRestriction : ContentKeyPolicyRestriction
    {

        public ContentKeyPolicyTokenRestriction(string issuer, string audience, string restrictionTokenType, ContentKeyPolicyVerificationKey primaryVerificationKey)
        {
            Issuer = issuer;
            Audience = audience;
            RestrictionTokenType = restrictionTokenType;
            PrimaryVerificationKey = primaryVerificationKey;
            AlternateVerificationKeys = new List<ContentKeyPolicyVerificationKey>();
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyTokenRestriction";

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("requiredClaims")]
        public List<RequiredClaim> RequiredClaims { get; set; }

        [JsonProperty("restrictionTokenType")]
        public string RestrictionTokenType { get; set; }

        [JsonProperty("primaryVerificationKey")]
        public ContentKeyPolicyVerificationKey PrimaryVerificationKey { get; set; }

        [JsonProperty("alternateVerificationKeys")]
        public List<ContentKeyPolicyVerificationKey> AlternateVerificationKeys { get; set; }

    }

    public class RequiredClaim
    {
        [JsonProperty("claimType")]
        public string ClaimType { get; set; }

        [JsonProperty("claimValue")]
        public string ClaimValue { get; set; }
    }

}
