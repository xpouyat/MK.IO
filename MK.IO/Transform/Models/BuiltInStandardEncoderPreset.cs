// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class BuiltInStandardEncoderPreset : EncoderPreset
    {

        public BuiltInStandardEncoderPreset(string presetName)
        {
            PresetName = presetName;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.BuiltInStandardEncoderPreset";

        [JsonProperty("presetName")]
        public string PresetName { get; set; }
    }
}