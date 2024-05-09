using System;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saveable.Converters;

public class SignalConverter : JsonConverter<Signal>
{
    public override Signal ReadJson(JsonReader reader, Type objectType, Signal existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var owner = serializer.Deserialize<GodotObject>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var name = serializer.Deserialize<StringName>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Signal(owner, name);
    }

    public override void WriteJson(JsonWriter writer, Signal value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("owner");
        serializer.Serialize(writer, value.Owner);

        writer.WritePropertyName("name");
        serializer.Serialize(writer, value.Name);

        writer.WriteEndObject();
    }
}