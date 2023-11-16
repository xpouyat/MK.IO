// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MK.IO
{
    public class JobInputHttp : JobInput
    {

        public JobInputHttp(string baseUri, List<string> files)
        {
            BaseUri = baseUri;
            Files = files;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.JobInputHttp";

        /// <summary>
        /// Base URI for HTTPS job input. It will be concatenated with provided file names. If no base uri is given, then the provided file list is assumed to be fully qualified uris.
        /// </summary>
        /// <value>Base URI for HTTPS job input. It will be concatenated with provided file names. If no base uri is given, then the provided file list is assumed to be fully qualified uris.</value>
        [JsonProperty("baseUri")]
        public string BaseUri { get; set; }

        /// <summary>
        /// Gets or Sets Start
        /// </summary>
        [JsonProperty("start")]
        public JobInputTime Start { get; set; }

        /// <summary>
        /// Gets or Sets End
        /// </summary>
        [JsonProperty("end")]
        public JobInputTime End { get; set; }

        /// <summary>
        /// List of files. Required for JobInputHttp.
        /// </summary>
        /// <value>List of files. Required for JobInputHttp.</value>
        [JsonProperty("files")]
        public List<string> Files { get; set; }
    }
}