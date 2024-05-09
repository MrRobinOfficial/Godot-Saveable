using System.Collections.Generic;
using Saveable.Extensions;

namespace Saveable;

/// <summary>
/// Store a collections of properties (<see cref="string"/> as key, <see cref="object"/> as value).
/// </summary>
public class NodeSave
{
    public static readonly NodeSave Empty = new NodeSave();

    /// <summary>
    /// Collection of <see cref="object"/>
    /// </summary>
    public Dictionary<string, object?> Properties { get; init; }

    public NodeSave() => Properties = new();

    public NodeSave(IDictionary<string, object?> properties) => Properties = new Dictionary<string, object?>(properties);

    /// <summary>
    /// Get the value of the property with the given <paramref name="key"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to get</param>
    /// <returns>Value of the property</returns>
    public T? GetProperty<T>(string key)
    {
        string json = SaveExtension.SerializeObject(Properties[key]);
        return SaveExtension.DeserializeObject<T>(json);
    }

    /// <summary>
    /// Try to get the value of the property with the given <paramref name="key"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to get</param>
    /// <param name="value">Value of the property, if the key exists</param>
    /// <returns><see langword="true"/> if the key exists, <see langword="false"/> otherwise</returns>
    public bool TryGetProperty<T>(string key, out T? value)
    {
        if (Properties.ContainsKey(key))
        {
            value = GetProperty<T>(key);
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Add a property with the given <paramref name="key"/> and <paramref name="value"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to add</param>
    /// <param name="value">Value of the property to add</param>
    public void AddProperty<T>(string key, T value) => Properties.Add(key, value!);

    /// <summary>
    /// Try to add a property with the given <paramref name="key"/> and <paramref name="value"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to add</param>
    /// <param name="value">Value of the property to add</param>
    /// <returns><see langword="true"/> if the property was added, <see langword="false"/> otherwise</returns>
    public bool TryAddProperty<T>(string key, T value) => Properties.TryAdd(key, value!);

    /// <summary>
    /// Add or update the property with the given <paramref name="key"/> and <paramref name="value"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to add or update</param>
    /// <param name="value">Value of the property to add or update</param>
    public void SetOrAddProperty<T>(string key, T value)
    {
        if (Properties.ContainsKey(key))
            Properties[key] = value;
        else
            Properties.Add(key, value);
    }

    /// <summary>
    /// Try to add or update the property with the given <paramref name="key"/> and <paramref name="value"/>
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    /// <param name="key">Key of the property to add or update</param>
    /// <param name="value">Value of the property to add or update</param>
    /// <returns><see langword="true"/> if the property was added or updated, <see langword="false"/> otherwise</returns>
    public bool TrySetOrAddProperty<T>(string key, T value)
    {
        if (Properties.ContainsKey(key))
        {
            Properties[key] = value;
            return true;
        }
        else
            return Properties.TryAdd(key, value);
    }

    /// <summary>
    /// Remove the property with the given <paramref name="key"/>
    /// </summary>
    /// <param name="key">Key of the property to remove</param>
    /// <returns><see langword="true"/> if the property was removed, <see langword="false"/> otherwise</returns>
    public bool RemoveProperty(string key) => Properties.Remove(key);
}