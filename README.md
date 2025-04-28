# UnityAdvancedMeshCreator
Fast mesh creator for Unity that allows to calculate mesh data separately in other thread.

## How to install
Go to [Releases](https://github.com/quad58/UnityAdvancedMeshCreator/releases) and download the archive. Then unpack the archive into your project's assets folder.

## Usage
```csharp
// You can put this in other thread.
MeshData meshData = new MeshData(verticies, triangles, normals, tangents, colors, uv);
// This you can put in main thread.
Mesh mesh = MeshCreator.CreateMesh(meshData);
```
