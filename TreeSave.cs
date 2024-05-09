using System.Collections.Generic;
using System.Linq;

namespace Saveable;

/// <summary>
/// Store a collections of saves (<see cref="string"/> as key, <see cref="NodeSave"/> as value).
/// </summary>
public class TreeSave
{
    public static readonly TreeSave Empty = new TreeSave();

    public TreeSave() => Collections = new Dictionary<string, NodeSave>();

    public TreeSave(IDictionary<string, NodeSave> collections) => Collections = new Dictionary<string, NodeSave>(collections);

    /// <summary>
    /// Collection of <see cref="NodeSave"/>
    /// </summary>
    public Dictionary<string, NodeSave> Collections { get; init; }

    /// <summary>
    /// The number of nodes in the collection
    /// </summary>
    public int NumOfNodes => Collections.Count;

    /// <summary>
    /// The number of properties in the collection
    /// </summary>
    public int NumOfProperties => Collections.Sum(x => x.Value.Properties.Count);

    /// <summary>
    /// Gets or sets the <see cref="NodeSave"/> associated with the given key
    /// </summary>
    /// <param name="key">The key of the <see cref="NodeSave"/></param>
    /// <returns>The <see cref="NodeSave"/> or <c>null</c> if not found</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/> is <c>null</c></exception>
    public NodeSave? this[string key]
    {
        get => Collections[key];
        set => Collections[key] = value!;
    }

    /// <summary>
    /// Gets the <see cref="NodeSave"/> with the given key from the collection
    /// </summary>
    /// <param name="key">The key to search for</param>
    /// <returns>The <see cref="NodeSave"/> or <c>null</c> if not found</returns>
    public NodeSave? GetCollection(string key) => Collections[key];

    /// <summary>
    /// Tries to get the <see cref="NodeSave"/> with the given key from the collection
    /// </summary>
    /// <param name="key">The key to search for</param>
    /// <param name="save">The <see cref="NodeSave"/> if found, otherwise <c>null</c></param>
    /// <returns><c>true</c> if the key was found, otherwise <c>false</c></returns>
    public bool TryGetCollection(string key, out NodeSave? save) => Collections.TryGetValue(key, out save);

    /// <summary>
    /// Adds a <see cref="NodeSave"/> to the collection with the given key
    /// </summary>
    /// <param name="key">The key to associate the <see cref="NodeSave"/> with</param>
    /// <param name="value">The <see cref="NodeSave"/> to add</param>
    public void AddCollection(string key, NodeSave value) => Collections.Add(key, value);

    /// <summary>
    /// Tries to add a <see cref="NodeSave"/> to the collection with the given key
    /// </summary>
    /// <param name="key">The key to associate the <see cref="NodeSave"/> with</param>
    /// <param name="value">The <see cref="NodeSave"/> to add</param>
    /// <returns><c>true</c> if the key was not already in the collection, otherwise <c>false</c></returns>
    public bool TryAddCollection(string key, NodeSave value) => Collections.TryAdd(key, value);

    /// <summary>
    /// Sets the <see cref="NodeSave"/> with the given key in the collection
    /// </summary>
    /// <param name="key">The key to search for</param>
    /// <param name="value">The <see cref="NodeSave"/> to set</param>
    public void SetCollection(string key, NodeSave value) => Collections[key] = value;

    /// <summary>
    /// Sets the <see cref="NodeSave"/> with the given key in the collection if the key already exists,
    /// otherwise adds the <see cref="NodeSave"/> to the collection with the given key
    /// </summary>
    /// <param name="key">The key to search for</param>
    /// <param name="value">The <see cref="NodeSave"/> to set or add</param>
    public void SetOrAddCollection(string key, NodeSave value)
    {
        if (Collections.ContainsKey(key))
            Collections[key] = value;
        else
            Collections.Add(key, value);
    }

    /// <summary>
    /// Tries to set the <see cref="NodeSave"/> with the given key in the collection if the key already exists,
    /// otherwise tries to add the <see cref="NodeSave"/> to the collection with the given key
    /// </summary>
    /// <param name="key">The key to search for</param>
    /// <param name="value">The <see cref="NodeSave"/> to set or add</param>
    /// <returns><c>true</c> if the key was found or added, otherwise <c>false</c></returns>
    public bool TrySetOrAddCollection(string key, NodeSave value)
    {
        if (Collections.ContainsKey(key))
        {
            Collections[key] = value;
            return true;
        }
        else
            return Collections.TryAdd(key, value);
    }

    /// <summary>
    /// Removes the <see cref="NodeSave"/> with the given key from the collection
    /// </summary>
    /// <param name="key">The key of the <see cref="NodeSave"/> to remove</param>
    /// <returns><c>true</c> if the key was found and removed, otherwise <c>false</c></returns>
    public bool RemoveCollection(string key) => Collections.Remove(key);
}