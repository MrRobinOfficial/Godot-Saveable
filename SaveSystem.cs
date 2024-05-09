using Godot;

using System.Collections.Generic;
using Saveable.Extensions;

namespace Saveable;

/// <summary>
/// A simple save system.
/// <list type="bullet|number|table">
///     <item>
///         <term>ISaveable</term>
///         <description>An interface to save and load a node</description>
///     </item>
///     <item>
///         <term>NodeSave</term>
///         <description>Store a collections of properties (<see cref="string"/> as key, <see cref="object"/> as value).</description>
///     </item>
///     <item>
///         <term>TreeSave</term>
///         <description>Stores a collections of <see cref="NodeSave"/>.</description>
///     </item>
/// </list>
/// <remarks>This system uses JSON (Newtonsoft.Json) for serialization.</remarks>
/// </summary>
public static class SaveSystem
{
    /// <summary>
    /// Loads a save file.
    /// </summary>
    /// <param name="filePath">Where the save file is located</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="loadTree">If the tree should be loaded. If false, only the <see cref="NodeSave"/> will be created.</param>
    /// <return>A <see cref="TreeSave"/>, which contains a collection of <see cref="NodeSave"/></return>
    public static TreeSave? LoadFile(string filePath, Node root, bool loadTree = true)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        return LoadFile(file, root, loadTree);
    }


    /// <summary>
    /// Loads a save file.
    /// </summary>
    /// <param name="filePath">Where the save file is located</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="compressionMode">The compression mode</param>
    /// <param name="loadTree">If the tree should be loaded. If false, only the <see cref="NodeSave"/> will be created.</param>
    /// <return>A <see cref="TreeSave"/>, which contains a collection of <see cref="NodeSave"/></return>
    /// <seealso cref="FileAccess.CompressionMode"/>
    public static TreeSave? LoadFile(string filePath, Node root, FileAccess.CompressionMode compressionMode, bool loadTree = true)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenCompressed(
            filePath,
            FileAccess.ModeFlags.Read,
            compressionMode
        );

        return LoadFile(file, root, loadTree);
    }

    /// <summary>
    /// Loads a save file.
    /// </summary>
    /// <param name="filePath">Where the save file is located</param>
    /// <param name="key">A key to decrypt the file</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="loadTree">If the tree should be loaded. If false, only the <see cref="NodeSave"/> will be created.</param>
    /// <return>A <see cref="TreeSave"/>, which contains a collection of <see cref="NodeSave"/></return>
    public static TreeSave? LoadFile(string filePath, byte[] key, Node root, bool loadTree = true)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenEncrypted(
            filePath,
            FileAccess.ModeFlags.Read,
            key
        );

        return LoadFile(file, root, loadTree);
    }

    /// <summary>
    /// Loads a save file.
    /// </summary>
    /// <param name="filePath">Where the save file is located</param>
    /// <param name="password">Password to decrypt the file</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="loadTree">If the tree should be loaded. If false, only the <see cref="NodeSave"/> will be created.</param>
    /// <return>A <see cref="TreeSave"/>, which contains a collection of <see cref="NodeSave"/></return>
    public static TreeSave? LoadFile(string filePath, string password, Node root, bool loadTree = true)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenEncryptedWithPass(
            filePath,
            FileAccess.ModeFlags.Read,
            password
        );

        return LoadFile(file, root, loadTree);
    }

    /// <summary>
    /// Saves a save file.
    /// </summary>
    /// <param name="filePath">Where to save the file to</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="treeSave">A <see cref="TreeSave"/> to save</param>
    public static void SaveFile(
        string filePath,
        Node root,
        TreeSave? treeSave = null)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
        SaveFile(file, root, treeSave);
    }

    /// <summary>
    /// Saves a save file.
    /// <para>NOTE; This will overwrite existing save file, if tree save is null.</para>
    /// </summary>
    /// <param name="filePath">Where to save the file to</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="compressionMode">The compression mode</param>
    /// <param name="treeSave">A <see cref="TreeSave"/> to save</param>
    public static void SaveFile(
        string filePath,
        Node root,
        FileAccess.CompressionMode compressionMode,
        TreeSave? treeSave = null)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenCompressed(
            filePath,
            FileAccess.ModeFlags.Write,
            compressionMode
        );

        SaveFile(file, root, treeSave);
    }

    /// <summary>
    /// Saves a save file.
    /// </summary>
    /// <param name="filePath">Where to save the file to</param>
    /// <param name="key">A key to decrypt the file</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="treeSave">A <see cref="TreeSave"/> to save</param>
    public static void SaveFile(
        string filePath,
        byte[] key,
        Node root,
        TreeSave? treeSave = null)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenEncrypted(
            filePath,
            FileAccess.ModeFlags.Write,
            key
        );

        SaveFile(file, root, treeSave);
    }

    /// <summary>
    /// Saves a save file.
    /// </summary>
    /// <param name="filePath">Where to save the file to</param>
    /// <param name="password">Password to encrypt the file</param>
    /// <param name="root">A node to search through the tree</param>
    /// <param name="treeSave">A <see cref="TreeSave"/> to save</param>
    public static void SaveFile(
        string filePath,
        string password,
        Node root,
        TreeSave? treeSave = null)
    {
        DirAccess.MakeDirRecursiveAbsolute(filePath.GetBaseDir());
        FileAccess file = FileAccess.OpenEncryptedWithPass(
            filePath,
            FileAccess.ModeFlags.Write,
            password
        );

        SaveFile(file, root, treeSave);
    }

    private static TreeSave? LoadFile(FileAccess file, Node root, bool loadTree = true)
    {
        if (file == null)
        {
            GD.PushWarning("File cannot be loaded!");
            return TreeSave.Empty;
        }

        if (root == null)
        {
            GD.PushWarning("Cannot load file without a root node!");
            return TreeSave.Empty;
        }

        string json = file.GetAsText();
        file.Close();

        if (string.IsNullOrEmpty(json))
        {
            GD.PushWarning("File is empty!");
            return TreeSave.Empty;
        }

        var save = SaveExtension.DeserializeObject<TreeSave>(json);

        if (loadTree)
            LoadTree(root, save);

#if TOOLS
        GD.Print($"File loaded!\n\nFound {save?.NumOfNodes} nodes.\nFound {save?.NumOfProperties} properties.");
#endif

        return save;
    }

    private static void LoadTree(Node root, TreeSave? treeSave)
    {
        if (treeSave == null)
            return;

        RunInChildrenRecursive(root, (ISaveable obj) =>
        {
            if (treeSave.TryGetCollection(obj.UniqueID, out NodeSave? nodeSave))
                obj.Load(nodeSave!);
        });
    }

    private static void SaveFile(
        FileAccess file,
        Node root,
        TreeSave? treeSave = null)
    {
        if (file == null)
        {
            GD.PushWarning("File cannot be saved!");
            return;
        }

        if (root == null)
        {
            GD.PushWarning("Cannot save a file without a root node!");
            return;
        }

        TreeSave save = SaveTree(root);

        if (treeSave != null)
        {
            foreach (KeyValuePair<string, NodeSave> collections in treeSave!.Collections)
                save.TryAddCollection(collections.Key, collections.Value);
        }

        string json = SaveExtension.SerializeObject(save);
        file.StoreString(json);

#if TOOLS
        GD.Print($"\nFile saved! Path located at: {file.GetPath()}.\n\nFound {save.NumOfNodes} nodes.\nFound {save.NumOfProperties} properties.");
#endif

        file.Close();
    }

    private static TreeSave SaveTree(Node root)
    {
        var tree = new TreeSave();
        RunInChildrenRecursive(root, (ISaveable obj) =>
        {
            var node = new NodeSave();
            obj.Save(node);
            tree.SetOrAddCollection(obj.UniqueID, node);
        });
        return tree;
    }

    private static void RunInChildrenRecursive<T>(
        Node parent,
        System.Action<T> action)
    {
        RunInChildrenRecursive(parent, (Node node) =>
        {
            if (node is not T t)
                return;

            action.Invoke(t);
        });
    }

    private static void RunInChildrenRecursive(
        Node parent,
        System.Action<Node> action)
    {
        RunInChildren(parent);

        void RunInChildren(Node parentNode)
        {
            foreach (Node node in parentNode.GetChildren())
            {
                if (node.GetChildCount() > 0)
                    RunInChildren(node);

                action.Invoke(node);
            }
        }
    }
}