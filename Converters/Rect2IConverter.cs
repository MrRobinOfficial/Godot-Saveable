using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Rect2IConverter : JsonConverter<Rect2I>
{
    public override Rect2I ReadJson(JsonReader reader, Type objectType, Rect2I existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var position = serializer.Deserialize<Vector2I>(reader);

        reader.Read(); // Read property's name
        var size = serializer.Deserialize<Vector2I>(reader);

        reader.Read(); // Read end object

        return new Rect2I(position, size);
    }

    public override void WriteJson(JsonWriter writer, Rect2I value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("position");
        serializer.Serialize(writer, value.Position);
        writer.WritePropertyName("size");
        serializer.Serialize(writer, value.Size);
        writer.WriteEndObject();
    }
}