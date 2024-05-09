using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Vector4Converter : JsonConverter<Vector4>
{
    public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
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

        reader.Read(); // Read property's name
        var z = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var w = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Vector4(x, y, z, w);
    }

    public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.X);
        writer.WritePropertyName("y");
        writer.WriteValue(value.Y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.Z);
        writer.WritePropertyName("w");
        writer.WriteValue(value.W);
        writer.WriteEndObject();
    }
}