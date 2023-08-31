// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class MKIOStreamingLocatorListPaths
    {
        public static MKIOStreamingLocatorListPaths FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MKIOStreamingLocatorListPaths>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("streamingPaths")]
        public List<StreamingPath> StreamingPaths { get; set; }

        [JsonProperty("downloadPaths")]
        public List<object> DownloadPaths { get; set; }

        [JsonProperty("drm")]
        public Drm Drm { get; set; }
    }


    public class Drm
    {
    }

    public class StreamingPath
    {
        [JsonProperty("streamingProtocol")]
        public string StreamingProtocol { get; set; }

        [JsonProperty("encryptionScheme")]
        public string EncryptionScheme { get; set; }

        [JsonProperty("paths")]
        public List<string> Paths { get; set; }
    }
}