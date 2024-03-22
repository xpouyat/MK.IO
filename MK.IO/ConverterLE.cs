// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace MK.IO
{
    internal static class ConverterLE
    {
        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters =
            {
                new CustomDateTimeConverter()
            },
        };
    }

    internal class CustomDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime)) || (objectType == typeof(DateTime?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
           if (objectType == typeof(DateTime?))
            {
                if (reader.Value == null)
                {
                    return (DateTime?)null;
                }
                return (DateTime?)reader.Value;
            }
            else if (objectType == typeof(DateTime))
            {
                return (DateTime)reader.Value;
            }

            return DateTime.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not null and DateTime)
            {
                // We need to reduce the precision. See issue https://github.com/xpouyat/MK.IO/issues/43
                // We limit the conversion of DateTime to the format "yyyy-MM-ddTHH:mm:ss.ffZ"
                writer.WriteValue(((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss.ffZ"));
            }
        }
    }
}