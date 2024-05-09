using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class RidConverter : JsonConverter<Rid>
{
    public override bool CanRead => false;

    // ((from is Resource resource) ? resource.GetRid()._id : 0);
    public override Rid ReadJson(JsonReader reader, Type objectType, Rid existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException();

        reader.Read(); // Read start object

        reader.Read(); // Read property's name
        var id = serializer.Deserialize<ulong>(reader);
        reader.Read(); // Read property's value

        reader.Read(); // Read end object

        GodotObject obj = GodotObject.InstanceFromId(id);
        return new Rid(obj);
    }

    public override void WriteJson(JsonWriter writer, Rid value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");
        writer.WriteValue(value.Id);
        writer.WriteEndObject();
    }
}