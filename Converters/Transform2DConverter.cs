using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class Transform2DConverter : JsonConverter<Transform2D>
{
    public override Transform2D ReadJson(JsonReader reader, Type objectType, Transform2D existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var origin = serializer.Deserialize<Vector2>(reader);

        reader.Read(); // Read property's name
        var rotation = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var scale = serializer.Deserialize<Vector2>(reader);

        reader.Read(); // Read property's name
        var skew = serializer.Deserialize<float>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Transform2D(Mathf.DegToRad(rotation), scale, Mathf.DegToRad(skew), origin);
    }

    public override void WriteJson(JsonWriter writer, Transform2D value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("origin");
        serializer.Serialize(writer, value.Origin);
        writer.WritePropertyName("rotation");
        serializer.Serialize(writer, Mathf.RadToDeg(value.Rotation));
        writer.WritePropertyName("scale");
        serializer.Serialize(writer, value.Scale);
        writer.WritePropertyName("skew");
        serializer.Serialize(writer, Mathf.RadToDeg(value.Skew));
        writer.WriteEndObject();
    }
}