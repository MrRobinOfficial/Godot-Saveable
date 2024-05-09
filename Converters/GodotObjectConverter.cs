using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class GodotObjectConverter : JsonConverter<GodotObject>
{
    public override GodotObject? ReadJson(JsonReader reader, Type objectType, GodotObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException();

        string? str = reader.Value as string;
        str = str!.Trim('<', '>');
        string[] arr = str!.Split('#');
        ulong id = ulong.Parse(arr[1]);
        return GodotObject.InstanceFromId(id);
    }

    public override void WriteJson(JsonWriter writer, GodotObject? value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToString());
    }
}