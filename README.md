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

#

## ⚙️ Requirements

* Godot 4.2.x Mono Version.
* Install [Newtonsoft.Json](https://www.newtonsoft.com/json) package.

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

## 📝 Quick guide

This plugin is parsing system for file type `JSON`. You can  <a href="https://en.wikipedia.org/wiki/JSON">read more</a> about `JSON` file format.

* `SaveSystem` is a static class that contains functions to load/save a file.
* `TreeSave` is a class that contains `NodeSave` for each node in the tree.
* `NodeSave` is class that contains a dictionary of key-value pairs.

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
