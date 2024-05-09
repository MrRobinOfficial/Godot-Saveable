using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Vector3IConverter : JsonConverter<Vector3I>
{
    public override Vector3I ReadJson(JsonReader reader, Type objectType, Vector3I existingValue, bool hasExistingValue, JsonSerializer serializer)
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

        reader.Read(); // Read property's name
        var z = serializer.Deserialize<int>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Vector3I(x, y, z);
    }

    public override void WriteJson(JsonWriter writer, Vector3I value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.X);
        writer.WritePropertyName("y");
        writer.WriteValue(value.Y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.Z);
        writer.WriteEndObject();
    }
}