using Godot;

namespace Saveable;

/// <summary>
/// An interface to save and load a node.
/// </summary>
public interface ISaveable
{
    /// <summary>
    /// A unique identifier of the node.
    /// <para>MUST BE UNIQUE!</para>
    /// </summary>
    internal abstract StringName UniqueID { get; }

    /// <summary>
    /// Load the node.
    /// </summary>
    /// <param name="save">Given <see cref="NodeSave"/> save</param>
    internal abstract void Load(NodeSave save);

    /// <summary>
    /// Save the node.
    /// </summary>
    /// <param name="save">Given <see cref="NodeSave"/> save</param>
    internal abstract void Save(NodeSave save);
}