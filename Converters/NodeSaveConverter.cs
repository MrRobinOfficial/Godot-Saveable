using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saveable.Converters;

public class NodeSaveConverter : JsonConverter<NodeSave>
{
    public override NodeSave? ReadJson(JsonReader reader, Type objectType, NodeSave? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            return null;

        var nodeSave = new NodeSave();

        while (reader.Read() && reader.TokenType != JsonToken.EndObject)
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                // position
                string? propertyName = reader.Value?.ToString();

                reader.Read(); // Move to property value

                object? propertyValue;

                // Check the token type to determine the appropriate deserialization logic
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        propertyValue = serializer.Deserialize<NodeSave>(reader);
                        break;
                    case JsonToken.StartArray:
                        propertyValue = serializer.Deserialize<List<object>>(reader);
                        break;

                    default:
                        propertyValue = reader.Value;
                        break;
                }

                nodeSave.AddProperty(propertyName!, propertyValue!);
            }
        }

        return nodeSave;
    }

    public override void WriteJson(JsonWriter writer, NodeSave? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, object> properties in value!.Properties)
        {
            writer.WritePropertyName(properties.Key);
            serializer.Serialize(writer, properties.Value);

            // using (var tokenReader = new JTokenReader(JToken.Parse(properties.Value)))
            // {
            //     while (tokenReader.Read())
            //     {
            //         writer.WriteToken(tokenReader.TokenType, tokenReader.Value);
            //     }
            // }
        }

        writer.WriteEndObject();
    }

}