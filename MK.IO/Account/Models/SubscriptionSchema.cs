// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class SubscriptionSchema
    {
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("createdById")]
        public string CreatedById { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; }

        [JsonProperty("azureSubscriptionName")]
        public string AzureSubscriptionName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("projectSubscriptionId")]
        public string ProjectSubscriptionId { get; set; }

        [JsonProperty("productPublicationId")]
        public string ProductPublicationId { get; set; }

        [JsonProperty("azureSubscriptionStatus")]
        public string AzureSubscriptionStatus { get; set; }

        [JsonProperty("azureSubscriptionId")]
        public string AzureSubscriptionId { get; set; }

        [JsonProperty("azureSubscriptionOfferId")]
        public string AzureSubscriptionOfferId { get; set; }

        [JsonProperty("azureSubscriptionPlanId")]
        public string AzureSubscriptionPlanId { get; set; }
    }
}