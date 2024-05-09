using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Transform3DConverter : JsonConverter<Transform3D>
{
    public override Transform3D ReadJson(JsonReader reader, Type objectType, Transform3D existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var basis = serializer.Deserialize<Basis>(reader);

        reader.Read(); // Read property's name
        var origin = serializer.Deserialize<Vector3>(reader);

        reader.Read(); // Read end object

        return new Transform3D(basis, origin);
    }

    public override void WriteJson(JsonWriter writer, Transform3D value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("basis");
        serializer.Serialize(writer, value.Basis);
        writer.WritePropertyName("origin");
        serializer.Serialize(writer, value.Origin);
        writer.WriteEndObject();
    }
}