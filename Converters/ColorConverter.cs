using System;
using Godot;
using Newtonsoft.Json;

namespace Saveable.Converters;

public class ColorConverter : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? str = reader.Value as string;
        return Color.FromHtml(str!);
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToHtml(includeAlpha: true));
    }
}