<!-- markdownlint-disable-next-line -->
<p align="center">
  <a href="#" rel="noopener" target="_blank"><img width="150" src="Icon.png" alt="Icon"></a>
</p>

<div align="center">

*A save system for Godot C#*
  
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/mrrobinofficial/unreal-iniparser/blob/HEAD/LICENSE.txt)
![plugin-status](https://img.shields.io/badge/plugin_status-ready_to_use-green)
![maintenance-status](https://img.shields.io/badge/maintenance-passively--maintained-yellowgreen.svg)

</div>

## 🔑 Features

- Saving entire node trees.
- Uses `Newtonsoft.Json` for serialization and deserialization.
- Supports both encrypted, compressed and regular mode.
- `ISaveable` interface to allow modular save/load structures.

## ⚙️ Requirements

* Godot 4.2.x Mono Version.
* Installed [Newtonsoft.Json](https://www.newtonsoft.com/json) package.

You can install **Newtonsoft.Json** via this command:

```console
dotnet add package Newtonsoft.Json
```

Or, you can install via [Nuget](https://www.nuget.org) [package manager](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio).

## ⚒️ Installation

You can install from the <a href="#">Godot asset library</a>.
Or you can install from the <a href="https://github.com/MrRobinOfficial/Godot-Saveable/releases/latest">release section</a>.

Alternatively, you can install this plugin via terminal with [*git*](https://git-scm.com/). **Here is the command for installing it**.

```console
cd addons
git clone git@github.com:MrRobinOfficial/Godot-Saveable.git Saveable
```

## 📦 Converters

This plugin is using [Newtonsoft.Json](https://www.newtonsoft.com/json) for serialization and deserialization. By using `JsonConverter`, it allows you to convert your custom classes to and from JSON. This plugin includes custom converters for Godot specific types.

Here's an list of available converters:

- Core converters
    - `NodeSaveConverter`
    - `TreeSaveConverter`
- Godot converters
    - `AabbConverter` - [Aabb](https://docs.godotengine.org/en/stable/classes/class_aabb.html)
    - `BasisConverter` - [Basis](https://docs.godotengine.org/en/stable/classes/class_basis.html)
    - `CallableConverter` - [Callable](https://docs.godotengine.org/en/stable/classes/class_callable.html)
    - `ColorConverter` - [Color](https://docs.godotengine.org/en/stable/classes/class_color.html)
    - `GodotObjectConverter` - [GodotObject](https://docs.godotengine.org/en/stable/classes/class_object.html)
    - `NodePathConverter` - [NodePath](https://docs.godotengine.org/en/stable/classes/class_nodepath.html)
    - `PlaneConverter` - [Plane](https://docs.godotengine.org/en/stable/classes/class_plane.html)
    - `ProjectionConverter` - [Projection](https://docs.godotengine.org/en/stable/classes/class_projection.html)
    - `QuaternionConverter` - [Quaternion](https://docs.godotengine.org/en/stable/classes/class_quaternion.html)
    - `Rect2Converter` - [Rect2](https://docs.godotengine.org/en/stable/classes/class_rect2.html)
    - `Rect2IConverter` - [Rect2I](https://docs.godotengine.org/en/stable/classes/class_rect2i.html)
    - `RidConverter` - [Rid](https://docs.godotengine.org/en/stable/classes/class_rid.html)
    - `SceneTreeConverter` - [SceneTree](https://docs.godotengine.org/en/stable/classes/class_scenetree.html)
    - `SignalConverter` - [Signal](https://docs.godotengine.org/en/stable/classes/class_signal.html)
    - `StringNameConverter` - [StringName](https://docs.godotengine.org/en/stable/classes/class_stringname.html)
    - `Transform2DConverter` - [Transform2D](https://docs.godotengine.org/en/stable/classes/class_transform2d.html)
    - `Transform3DConverter` - [Transform3D](https://docs.godotengine.org/en/stable/classes/class_transform3d.html)
    - `Vector2Converter` - [Vector2](https://docs.godotengine.org/en/stable/classes/class_vector2.html)
    - `Vector2IConverter` - [Vector2I](https://docs.godotengine.org/en/stable/classes/class_vector2i.html)
    - `Vector3Converter` - [Vector3](https://docs.godotengine.org/en/stable/classes/class_vector3.html)
    - `Vector3IConverter` - [Vector3I](https://docs.godotengine.org/en/stable/classes/class_vector3i.html)
    - `Vector4Converter` - [Vector4](https://docs.godotengine.org/en/stable/classes/class_vector4.html)
    - `Vector4IConverter` - [Vector4I](https://docs.godotengine.org/en/stable/classes/class_vector4i.html)

## 📝 Quick guide

This plugin uses `JSON` as a serialization format. You can <a href="https://en.wikipedia.org/wiki/JSON">read more</a> about `JSON` file format.

* `SaveSystem` is a static class that contains functions to load/save a file.
* `TreeSave` is a class that contains `NodeSave` for each node in the tree.
* `NodeSave` is class that contains a dictionary of key-value pairs.
* `ISaveable` is an interface that defines a function `Load(NodeSave save)` and `Save(NodeSave save)`.

Use the `ISaveable` interface for callbacks related to save system:

```csharp
public partial class App : Node, ISaveable
{
    StringName ISaveable.UniqueID => "app";

    private float _globalVolume = 1.0f;
    private float _musicVolume = 0.5f;
    private float _effectsVolume = 0.8f;
    private float _motionBlurEffect = 0.0f;
    private float _postProcessingEffect = 1.0f;

    void ISaveable.Load(NodeSave save)
    {
        _globalVolume = save.GetProperty<float>("globalVolume");
        _musicVolume = save.GetProperty<float>("musicVolume");
        _effectsVolume = save.GetProperty<float>("effectsVolume");
        _motionBlurEffect = save.GetProperty<float>("motionBlurEffect");
        _postProcessingEffect = save.GetProperty<float>("postProcessingEffect");
    }

    void ISaveable.Save(NodeSave save)
    {
        save.AddProperty("globalVolume", _globalVolume);
        save.AddProperty("musicVolume", _musicVolume);
        save.AddProperty("effectsVolume", _effectsVolume);
        save.AddProperty("motionBlurEffect", _motionBlurEffect);
        save.AddProperty("postProcessingEffect", _postProcessingEffect);
    }
}
```

Output:

```json
{
  "app": {
    "globalVolume": 1.0,
    "musicVolume": 0.5,
    "effectsVolume": 0.8,
    "motionBlurEffect": 0.0,
    "postProcessingEffect": 1.0
  }
}
```

If you want to add JSON property without messing with **Newtonsoft.Json** and **internal** systems, you can instead use `System.Dynamic.ExpandoObject` and `dynamic` keyword.

Here's the modified version:

```csharp
public partial class App : Node, ISaveable
{
    StringName ISaveable.UniqueID => "app";

    private float _globalVolume = 1.0f;
    private float _musicVolume = 0.5f;
    private float _effectsVolume = 0.8f;
    private float _motionBlurEffect = 0.0f;
    private float _postProcessingEffect = 1.0f;

    void ISaveable.Load(NodeSave save)
    {
        dynamic? volumes = save.GetProperty<dynamic>("volumes");
        _globalVolume = volumes?.global;
        _musicVolume = volumes?.music;
        _effectsVolume = volumes?.effects;

        dynamic? graphics = save.GetProperty<dynamic>("graphics");
        _motionBlurEffect = graphics?.motionBlur;
        _postProcessingEffect = graphics?.postProcessing;
    }

    void ISaveable.Save(NodeSave save)
    {
        dynamic volumes = new System.Dynamic.ExpandoObject();
        volumes.global = _globalVolume;
        volumes.music = _musicVolume;
        volumes.effects = _effectsVolume;

        dynamic graphics = new System.Dynamic.ExpandoObject();
        graphics.motionBlur = _motionBlurEffect;
        graphics.postProcessing = _postProcessingEffect;

        save.AddProperty("volumes", volumes);
        save.AddProperty("graphics", graphics);
    }
}
```

Output:

```json
{
  "app": {
    "volumes": {
      "global": 1.0,
      "music": 0.5,
      "effects": 0.8
    },
    "graphics": {
      "motionBlur": 0.0,
      "postProcessing": 1.0
    },
  }
}
```

#

Load a file:

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";

// Loads the file.
SaveSystem.LoadFile(
    FILE_PATH,
    GetTree().Root
);
```

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";
private FileAccess.CompressionMode COMPRESSION = FileAccess.CompressionMode.Fastlz;

// Loads the file with a compression method.
SaveSystem.LoadFile(
    FILE_PATH,
    GetTree().Root,
    COMPRESSION
);
```

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";
private string PASSWORD = "abc123";

// Loads the file with a protected password.
SaveSystem.LoadFile(
    FILE_PATH,
    GetTree().Root,
    PASSWORD
);
```

Save a file:

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";

// Saves the file.
SaveSystem.SaveFile(
    FILE_PATH,
    GetTree().Root
);
```

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";
private FileAccess.CompressionMode COMPRESSION = FileAccess.CompressionMode.Fastlz;

// Saves the file with a compression method.
SaveSystem.SaveFile(
    FILE_PATH,
    GetTree().Root,
    COMPRESSION
);
```

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";
private string PASSWORD = "abc123";

// Saves the file with a protected password.
SaveSystem.SaveFile(
    FILE_PATH,
    GetTree().Root,
    PASSWORD
);
```

> [!NOTE]
> If you wish to load a save file before saving it, you must specify to disable the auto load on `LoadFile()` method.

Here's an example:

```csharp
private string FILE_PATH = "user://saves/profile_01/save.dat";

// Create a TreeSave without loading it into memory.
// Very useful when using SaveFile(), as it enables applying only the modifications made.
TreeSave? save = SaveSystem.LoadFile(
    FILE_PATH,
    GetTree().Root,
    loadTree: false
);

// Saves the TreeSave to a file.
// By specifying a 'save' parameter, it appends changes to an existing save.
SaveSystem.SaveFile(
    FILE_PATH,
    GetTree().Root,
    save // Optional parameter to append changes to an existing save
);
```

#

Here's a practical example:

```csharp
public partial class SaveMenu : CanvasLayer
{
    [Export] public Button? LoadBtn { get; private set; }
    [Export] public Button? SaveBtn { get; private set; }

    public override void _EnterTree()
    {
        LoadBtn!.Pressed += OnPressed_Load;
        SaveBtn!.Pressed += OnPressed_Save;
    }

    public override void _ExitTree()
    {
        LoadBtn!.Pressed -= OnPressed_Load;
        SaveBtn!.Pressed -= OnPressed_Save;
    }

    private string FILE_PATH = "user://saves/profile_01/save.dat";

    private void OnPressed_Load()
    {
        SaveSystem.LoadFile(
            FILE_PATH,
            GetTree().Root
        );
    }

    private void OnPressed_Save()
    {
        TreeSave? save = SaveSystem.LoadFile(
            FILE_PATH,
            GetTree().Root,
            loadTree: false
        );

        SaveSystem.SaveFile(
            FILE_PATH,
            GetTree().Root,
            save
        );
    }
}
```

## 🆘 Support
If you have any questions or issue, just write either to my [YouTube channel](https://www.youtube.com/@mrrobinofficial), [Email](mailto:mrrobin123mail@gmail.com) or [Twitter DM](https://twitter.com/MrRobinOfficial).

## 🔗 References
- [Installing plugins](https://docs.godotengine.org/en/stable/tutorials/plugins/editor/installing_plugins.html)
- [Introduction to JSON file format](https://en.wikipedia.org/wiki/JSON)
- [Nuget](https://www.nuget.org)
- [Nuget package manager](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [git](https://git-scm.com/)
- [Aabb](https://docs.godotengine.org/en/stable/classes/class_aabb.html)
- [Basis](https://docs.godotengine.org/en/stable/classes/class_basis.html)
- [Callable](https://docs.godotengine.org/en/stable/classes/class_callable.html)
- [Color](https://docs.godotengine.org/en/stable/classes/class_color.html)
- [GodotObject](https://docs.godotengine.org/en/stable/classes/class_object.html)
- [NodePath](https://docs.godotengine.org/en/stable/classes/class_nodepath.html)
- [Plane](https://docs.godotengine.org/en/stable/classes/class_plane.html)
- [Projection](https://docs.godotengine.org/en/stable/classes/class_projection.html)
- [Quaternion](https://docs.godotengine.org/en/stable/classes/class_quaternion.html)
- [Rect2](https://docs.godotengine.org/en/stable/classes/class_rect2.html)
- [Rect2I](https://docs.godotengine.org/en/stable/classes/class_rect2i.html)
- [Rid](https://docs.godotengine.org/en/stable/classes/class_rid.html)
- [SceneTree](https://docs.godotengine.org/en/stable/classes/class_scenetree.html)
- [Signal](https://docs.godotengine.org/en/stable/classes/class_signal.html)
- [StringName](https://docs.godotengine.org/en/stable/classes/class_stringname.html)
- [Transform2D](https://docs.godotengine.org/en/stable/classes/class_transform2d.html)
- [Transform3D](https://docs.godotengine.org/en/stable/classes/class_transform3d.html)
- [Vector2](https://docs.godotengine.org/en/stable/classes/class_vector2.html)
- [Vector2I](https://docs.godotengine.org/en/stable/classes/class_vector2i.html)
- [Vector3](https://docs.godotengine.org/en/stable/classes/class_vector3.html)
- [Vector3I](https://docs.godotengine.org/en/stable/classes/class_vector3i.html)
- [Vector4](https://docs.godotengine.org/en/stable/classes/class_vector4.html)
- [Vector4I](https://docs.godotengine.org/en/stable/classes/class_vector4i.html)
