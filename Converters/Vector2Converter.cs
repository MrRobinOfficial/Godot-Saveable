using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var x = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var y = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Vector2(x, y);
    }

    public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.X);
        writer.WritePropertyName("y");
        writer.WriteValue(value.Y);
        writer.WriteEndObject();
    }
}