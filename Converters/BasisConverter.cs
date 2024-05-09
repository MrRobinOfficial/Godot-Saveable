using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class BasisConverter : JsonConverter<Basis>
{
    public override Basis ReadJson(JsonReader reader, Type objectType, Basis existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartArray)
            throw new JsonSerializationException();

        reader.Read(); // Read start array

        var column0 = serializer.Deserialize<Vector3>(reader);
        var column1 = serializer.Deserialize<Vector3>(reader);
        var column2 = serializer.Deserialize<Vector3>(reader);

        reader.Read(); // Read end array

        return new Basis(column0, column1, column2);
    }

    public override void WriteJson(JsonWriter writer, Basis value, JsonSerializer serializer)
    {
        writer.WriteStartArray();
        serializer.Serialize(writer, value.Column0);
        serializer.Serialize(writer, value.Column1);
        serializer.Serialize(writer, value.Column2);
        writer.WriteEndArray();
    }
}