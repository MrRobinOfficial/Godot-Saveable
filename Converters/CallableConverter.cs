using System;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Saveable.Converters;

public class CallableConverter : JsonConverter<Callable>
{
    public override Callable ReadJson(JsonReader reader, Type objectType, Callable existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var target = serializer.Deserialize<GodotObject>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read property's name
        var method = serializer.Deserialize<StringName>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        return new Callable(target, method);
    }

    public override void WriteJson(JsonWriter writer, Callable value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("target");
        serializer.Serialize(writer, value.Target);

        writer.WritePropertyName("method");
        serializer.Serialize(writer, value.Method);

        writer.WriteEndObject();
    }
}