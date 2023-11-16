// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    public class BuiltInStandardEncoderPreset : TransformPreset
    {

        public BuiltInStandardEncoderPreset(EncoderNamedPreset presetName)
        {
            PresetName = presetName;
        }

        [JsonProperty("@odata.type")]
        internal string OdataType => "#Microsoft.Media.BuiltInStandardEncoderPreset";

        [JsonProperty("presetName")]
        public EncoderNamedPreset PresetName { get; set; }
    }
}