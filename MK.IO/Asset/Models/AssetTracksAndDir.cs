// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO.Models
{

    public class AssetTracksAndDir
    {
        public static AssetTracksAndDir FromJson(string json)
        {
            return JsonConvert.DeserializeObject<AssetTracksAndDir>(json, ConverterLE.Settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ConverterLE.Settings);
        }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("spec")]
        public Spec Spec { get; set; }
    }

    public class Audio
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("systemBitrate")]
        public string SystemBitrate { get; set; }

        [JsonProperty("systemLanguage")]
        public string SystemLanguage { get; set; }

        [JsonProperty("trackID")]
        public string TrackID { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }
    }

    public class Body
    {
        [JsonProperty("textstream")]
        public List<Textstream> Textstream { get; set; }

        [JsonProperty("video")]
        public List<Video> Video { get; set; }

        [JsonProperty("audio")]
        public List<Audio> Audio { get; set; }
    }



    public class Extra
    {
    }

    public class Files
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }

    public class Folders
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Head
    {
        [JsonProperty("clientManifestRelativePath")]
        public string ClientManifestRelativePath { get; set; }

        [JsonProperty("availabilityStartTime")]
        public DateTime AvailabilityStartTime { get; set; }

        [JsonProperty("compatVersion")]
        public string CompatVersion { get; set; }

        [JsonProperty("formats")]
        public string Formats { get; set; }

        [JsonProperty("fragmentsPerHLSSegment")]
        public string FragmentsPerHLSSegment { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }


    public class Spec
    {
        [JsonProperty("container")]
        public string Container { get; set; }

        [JsonProperty("folders")]
        public Folders Folders { get; set; }

        [JsonProperty("files")]
        public Files Files { get; set; }

        [JsonProperty("tracks")]
        public Tracks Tracks { get; set; }


    }

    public class Textstream
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("systemBitrate")]
        public string SystemBitrate { get; set; }

        [JsonProperty("manifestOutput")]
        public string ManifestOutput { get; set; }

        [JsonProperty("trackID")]
        public string TrackID { get; set; }

        [JsonProperty("parentTrackName")]
        public string ParentTrackName { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }

        [JsonProperty("timescale")]
        public string Timescale { get; set; }

        [JsonProperty("Scheme")]
        public string Scheme { get; set; }
    }

    public class Tracks
    {
        [JsonProperty("additionalProp")]
        public AdditionalProp AdditionalProp { get; set; }
    }

    public class AdditionalProp
    {
        [JsonProperty("head")]
        public Head Head { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }
    }

    public class Video
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("systemBitrate")]
        public string SystemBitrate { get; set; }

        [JsonProperty("systemLanguage")]
        public string SystemLanguage { get; set; }

        [JsonProperty("trackID")]
        public string TrackID { get; set; }

        [JsonProperty("trackName")]
        public string TrackName { get; set; }
    }


}