using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Vector2IConverter : JsonConverter<Vector2I>
{
    public override Vector2I ReadJson(JsonReader reader, Type objectType, Vector2I existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var x = serializer.Deserialize<int>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var y = serializer.Deserialize<int>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Vector2I(x, y);
    }

    public override void WriteJson(JsonWriter writer, Vector2I value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.X);
        writer.WritePropertyName("y");
        writer.WriteValue(value.Y);
        writer.WriteEndObject();
    }
}