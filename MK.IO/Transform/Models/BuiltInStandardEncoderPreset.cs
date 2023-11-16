// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class BuiltInStandardEncoderPreset : TransformOutputPreset
    {

        public BuiltInStandardEncoderPreset(string presetName)
        {
            PresetName = presetName;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.BuiltInStandardEncoderPreset";

        // Use EncoderNamedPreset to list possible values for this property
        [JsonProperty("presetName")]
        public string PresetName { get; set; }
    }
}