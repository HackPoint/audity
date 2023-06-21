using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Common.JsonConverters;

public class StringJsonConverter : JsonConverter<String> {
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString();

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Replace("\"", ""));
}