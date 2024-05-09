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

## ⚙️ Supported Platforms
This plug-in was last built against Godot 4.2.2 mono version.

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

Here's a simple way to use this plugin:

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

#

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

## 🆘 Support
If you have any questions or issue, just write either to my [YouTube channel](https://www.youtube.com/@mrrobinofficial), [Email](mailto:mrrobin123mail@gmail.com) or [Twitter DM](https://twitter.com/MrRobinOfficial).

## 🔗 References
- [Installing plugins](https://docs.godotengine.org/en/stable/tutorials/plugins/editor/installing_plugins.html)
- [Introduction to JSON file format](https://en.wikipedia.org/wiki/JSON)