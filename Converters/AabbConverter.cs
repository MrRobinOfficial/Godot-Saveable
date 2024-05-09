using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class AabbConverter : JsonConverter<Aabb>
{
    public override Aabb ReadJson(JsonReader reader, Type objectType, Aabb existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var position = serializer.Deserialize<Vector3>(reader);

        reader.Read(); // Read property's name
        var size = serializer.Deserialize<Vector3>(reader);

        reader.Read(); // Read end object

        return new Aabb(position, size);
    }

    public override void WriteJson(JsonWriter writer, Aabb value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("position");
        writer.WriteValue(value.Position);

        writer.WritePropertyName("size");
        writer.WriteValue(value.Size);

        writer.WriteEndObject();
    }
}