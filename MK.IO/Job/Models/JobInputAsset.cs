// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class JobInputAsset : JobInput
    {

        public JobInputAsset(string assetName, List<string> files)
        {
            AssetName = assetName;
            Files = files;
        }

        [JsonProperty("@odata.type")]
        internal override string OdataType => "#Microsoft.Media.JobInputAsset";

        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("files")]
        public List<string> Files { get; set; }

        /// <summary>
        /// Defines a point on the timeline of the input media at which processing will start. Defaults to the beginning of the input media.
        /// </summary>
        [JsonProperty("start")]
        public JobInputTime? Start { get; set; }

        /// <summary>
        /// Defines a point on the timeline of the input media at which processing will end. Defaults to the end of the input media.
        /// </summary>
        [JsonProperty("end")]
        public JobInputTime? End { get; set; }

    }
}