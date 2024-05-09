using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class NodePathConverter : JsonConverter<NodePath>
{
    public override NodePath? ReadJson(JsonReader reader, Type objectType, NodePath? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        string? path = reader.Value as string;
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new NodePath(path);
    }

    public override void WriteJson(JsonWriter writer, NodePath? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("nodePath");
        writer.WriteValue(value?.ToString());
        writer.WriteEndObject();
    }
}