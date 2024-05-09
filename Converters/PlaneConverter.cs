using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class PlaneConverter : JsonConverter<Plane>
{
    public override Plane ReadJson(JsonReader reader, Type objectType, Plane existingValue, bool hasExistingValue, JsonSerializer serializer)
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
        var d = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Plane(x, y, z, d);
    }

    public override void WriteJson(JsonWriter writer, Plane value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.X);
        writer.WritePropertyName("y");
        writer.WriteValue(value.Y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.Z);
        writer.WritePropertyName("distance");
        writer.WriteValue(value.D);
        writer.WriteEndObject();
    }
}