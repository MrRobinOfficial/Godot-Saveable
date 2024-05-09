using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Rect2Converter : JsonConverter<Rect2>
{
    public override Rect2 ReadJson(JsonReader reader, Type objectType, Rect2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var position = serializer.Deserialize<Vector2>(reader);

        reader.Read(); // Read property's name
        var size = serializer.Deserialize<Vector2>(reader);

        reader.Read(); // Read end object

        return new Rect2(position, size);
    }

    public override void WriteJson(JsonWriter writer, Rect2 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("position");
        serializer.Serialize(writer, value.Position);

        writer.WritePropertyName("size");
        serializer.Serialize(writer, value.Size);

        writer.WriteEndObject();
    }
}