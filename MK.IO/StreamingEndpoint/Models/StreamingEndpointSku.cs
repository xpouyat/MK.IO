// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class StreamingEndpointSku
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Standard or Premium</param>
        /// <param name="capacity">600</param>
        public StreamingEndpointSku(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }
}