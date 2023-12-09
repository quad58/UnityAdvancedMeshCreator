using UnityEngine;
using UnityEngine.Rendering;

namespace AdvancedMeshCreator
{
    public class MeshCreator
    {
        /// <summary>
        /// Create mesh object from MeshData.
        /// </summary>
        /// <returns>Created mesh object from MeshData.</returns>
        public static Mesh CreateMesh(MeshData data)
        {
            Mesh mesh = new Mesh();

            VertexAttributeDescriptor[] layout = new VertexAttributeDescriptor[]
            {
                new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3),
                new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3),
                new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4),
                new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
                new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2),
            };
            
            mesh.SetVertexBufferParams(data.Verticies.Length, layout);

            mesh.SetVertexBufferData(data.Verticies, 0, 0, data.Verticies.Length);

            mesh.SetIndexBufferParams(data.Triangles.Length, IndexFormat.UInt32);
            mesh.SetIndexBufferData(data.Triangles, 0, 0, data.Triangles.Length);

            mesh.subMeshCount = 1;
            mesh.SetSubMesh(0, new SubMeshDescriptor(0, data.Triangles.Length));

            mesh.bounds = data.Bounds;

            return mesh;
        }
    }
}
