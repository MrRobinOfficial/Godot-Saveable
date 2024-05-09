using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class StringNameConverter : JsonConverter<StringName>
{
    public override StringName? ReadJson(JsonReader reader, Type objectType, StringName? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException();

        string? str = reader.Value as string;
        return new StringName(str);
    }

    public override void WriteJson(JsonWriter writer, StringName? value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToString());
    }
}