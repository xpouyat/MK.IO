// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MK.IO
{
    /// <summary> The built-in preset to be used for encoding videos. </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EncoderNamedPreset
    {
        /// <summary>
        /// Enum H264SingleBitrateSD for value: H264SingleBitrateSD
        /// </summary>
        [EnumMember(Value = "H264SingleBitrateSD")]
        H264SingleBitrateSD = 1,

        /// <summary>
        /// Enum H264SingleBitrate720p for value: H264SingleBitrate720p
        /// </summary>
        [EnumMember(Value = "H264SingleBitrate720p")]
        H264SingleBitrate720p = 2,

        /// <summary>
        /// Enum H264SingleBitrate1080p for value: H264SingleBitrate1080p
        /// </summary>
        [EnumMember(Value = "H264SingleBitrate1080p")]
        H264SingleBitrate1080p = 3,

        /// <summary>
        /// Enum H264MultipleBitrateSD for value: H264MultipleBitrateSD
        /// </summary>
        [EnumMember(Value = "H264MultipleBitrateSD")]
        H264MultipleBitrateSD = 4,

        /// <summary>
        /// Enum H264MultipleBitrate720p for value: H264MultipleBitrate720p
        /// </summary>
        [EnumMember(Value = "H264MultipleBitrate720p")]
        H264MultipleBitrate720p = 5,

        /// <summary>
        /// Enum H264MultipleBitrate1080p for value: H264MultipleBitrate1080p
        /// </summary>
        [EnumMember(Value = "H264MultipleBitrate1080p")]
        H264MultipleBitrate1080p = 6,

        /// <summary>
        /// Enum H265SingleBitrate720p for value: H265SingleBitrate720p
        /// </summary>
        [EnumMember(Value = "H265SingleBitrate720p")]
        H265SingleBitrate720p = 7,

        /// <summary>
        /// Enum H265SingleBitrate1080p for value: H265SingleBitrate1080p
        /// </summary>
        [EnumMember(Value = "H265SingleBitrate1080p")]
        H265SingleBitrate1080p = 8,

        /// <summary>
        /// Enum H265SingleBitrate4K for value: H265SingleBitrate4K
        /// </summary>
        [EnumMember(Value = "H265SingleBitrate4K")]
        H265SingleBitrate4K = 9,

        /// <summary>
        /// Enum AACGoodQualityAudio for value: AACGoodQualityAudio
        /// </summary>
        [EnumMember(Value = "AACGoodQualityAudio")]
        AACGoodQualityAudio = 10
    }
}