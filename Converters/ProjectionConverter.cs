using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class ProjectionConverter : JsonConverter<Projection>
{
    public override Projection ReadJson(JsonReader reader, Type objectType, Projection existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var x = serializer.Deserialize<Vector4>(reader);

        reader.Read(); // Read property's name
        var y = serializer.Deserialize<Vector4>(reader);

        reader.Read(); // Read property's name
        var z = serializer.Deserialize<Vector4>(reader);

        reader.Read(); // Read property's name
        var w = serializer.Deserialize<Vector4>(reader);

        reader.Read(); // Read end object

        return new Projection(x, y, z, w);
    }

    public override void WriteJson(JsonWriter writer, Projection value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        serializer.Serialize(writer, value.X);
        writer.WritePropertyName("y");
        serializer.Serialize(writer, value.Y);
        writer.WritePropertyName("z");
        serializer.Serialize(writer, value.Z);
        writer.WritePropertyName("w");
        serializer.Serialize(writer, value.W);
        writer.WriteEndObject();
    }
}