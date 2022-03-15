﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord.JsonConverters;

internal class StringEnumConverterWithErrorHandling : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum && (int)Type.GetTypeCode(typeToConvert) % 2 == 1;

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => (JsonConverter)Activator.CreateInstance(typeof(EnumConverterWithErrorHandling<>).MakeGenericType(typeToConvert))!;

    private class EnumConverterWithErrorHandling<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse<T>(reader.GetString(), true, out var result))
                return result;
            else
                return (T)Enum.ToObject(typeToConvert, -1);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var v = value.ToString();
            writer.WriteStringValue(v[0] >= 'A' ? v.ToLowerInvariant() : throw new System.ComponentModel.InvalidEnumArgumentException($"Type: {typeof(T)}"));
        }

        public override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Read(ref reader, typeToConvert, options);

        public override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var v = value.ToString();
            writer.WritePropertyName(v[0] >= 'A' ? v.ToLowerInvariant() : throw new System.ComponentModel.InvalidEnumArgumentException($"Type: {typeof(T)}"));
        }
    }
}