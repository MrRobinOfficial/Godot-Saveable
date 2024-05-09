using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class SceneTreeConverter : JsonConverter<SceneTree>
{
    public override SceneTree? ReadJson(JsonReader reader, Type objectType, SceneTree? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var root = serializer.Deserialize<Node>(reader);
        reader.Read(); // Read property's value

        while (reader.TokenType != JsonToken.EndObject)
            reader.Read();

        return root?.GetChildCount() > 0 ? root!.GetChild(0).GetTree() : null;
    }

    public override void WriteJson(JsonWriter writer, SceneTree? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("root");
        serializer.Serialize(writer, value!.Root);

        writer.WritePropertyName("nodes");
        writer.WriteStartArray();

        foreach (Node node in value!.Root.GetChildren())
            serializer.Serialize(writer, node);

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}