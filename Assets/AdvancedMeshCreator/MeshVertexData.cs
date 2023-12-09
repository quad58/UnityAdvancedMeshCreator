using System.Runtime.InteropServices;
using UnityEngine;

namespace AdvancedMeshCreator
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MeshVertexData
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 Tangent;
        public Color32 Color;
        public Vector2 UV;
    }
}
