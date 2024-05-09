using Godot;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saveable.Converters;

public class TreeSaveConverter : JsonConverter<TreeSave>
{
    public override TreeSave? ReadJson(JsonReader reader, Type objectType, TreeSave? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        var tree = new TreeSave();

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                string? id = reader.Value?.ToString();

                // Reads the value
                reader.Read();

                NodeSave? save = serializer.Deserialize<NodeSave>(reader);
                tree.SetOrAddCollection(id!, save!);
            }
        }

        return tree;
    }

    public override void WriteJson(JsonWriter writer, TreeSave? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        foreach (var collection in value!.Collections)
        {
            writer.WritePropertyName(collection.Key);
            serializer.Serialize(writer, collection.Value);
        }

        writer.WriteEndObject();
    }

}