using UnityEngine;

namespace AdvancedMeshCreator
{
    public class MeshData
    {
        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector3[] normals, Vector4[] tangents, Color[] colors, Vector2[] uv)
        {
            Verticies = GetMeshVertexDatas(verticies, normals, tangents, colors, uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector3[] normals, Color[] colors, Vector2[] uv)
        {
            Verticies = GetMeshVertexDatas(verticies, normals, GetTangents(verticies, normals), colors, uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector4[] tangents, Color[] colors, Vector2[] uv)
        {
            Verticies = GetMeshVertexDatas(verticies, GetNormals(verticies, triangles), tangents, colors, uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Color[] colors, Vector2[] uv)
        {
            Vector3[] normals = GetNormals(verticies, triangles);
            Verticies = GetMeshVertexDatas(verticies, normals, GetTangents(verticies, normals), colors, uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector4[] tangents, Vector2[] uv)
        {
            Verticies = GetMeshVertexDatas(verticies, GetNormals(verticies, triangles), tangents, new Color[verticies.Length], uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector2[] uv)
        {
            Vector3[] normals = GetNormals(verticies, triangles);
            Verticies = GetMeshVertexDatas(verticies, normals, GetTangents(verticies, normals), new Color[verticies.Length], uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector4[] tangents, Color[] colors)
        {
            Verticies = GetMeshVertexDatas(verticies, GetNormals(verticies, triangles), tangents, colors, new Vector2[verticies.Length]);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Color[] colors)
        {
            Vector3[] normals = GetNormals(verticies, triangles);
            Vector2[] uv = new Vector2[verticies.Length];
            Verticies = GetMeshVertexDatas(verticies, normals, GetTangents(verticies, normals), colors, uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles, Vector4[] tangents)
        {
            Verticies = GetMeshVertexDatas(verticies, GetNormals(verticies, triangles), tangents, new Color[verticies.Length], new Vector2[verticies.Length]);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Create MeshData object and calculate it.
        /// </summary>
        public MeshData(Vector3[] verticies, int[] triangles)
        {
            Vector3[] normals = GetNormals(verticies, triangles);
            Vector2[] uv = new Vector2[verticies.Length];
            Verticies = GetMeshVertexDatas(verticies, normals, GetTangents(verticies, normals), new Color[verticies.Length], uv);
            Triangles = triangles;
            Bounds = GetBounds(verticies);
        }

        /// <summary>
        /// Calculate MeshVertexData array.
        /// </summary>
        /// <returns>Calculated MeshVertexData array.</returns>
        public static MeshVertexData[] GetMeshVertexDatas(Vector3[] verticies, Vector3[] normals, Vector4[] tangents, Color[] colors, Vector2[] uv)
        {
            MeshVertexData[] vertexDatas = new MeshVertexData[verticies.Length];

            for (int i = 0; i < vertexDatas.Length; i++)
            {
                vertexDatas[i].Position = verticies[i];
                vertexDatas[i].Normal = normals[i];
                vertexDatas[i].Tangent = tangents[i];
                vertexDatas[i].UV = uv[i];
                vertexDatas[i].Color = colors[i];
            }

            return vertexDatas;
        }

        /// <summary>
        /// Calculate tangents array.
        /// </summary>
        /// <returns>Calculated Vector4 array of tangents.</returns>
        public static Vector4[] GetTangents(Vector3[] verticies, Vector3[] normals)
        {
            Vector4[] tangents = new Vector4[verticies.Length];
            for (int i = 0; i < tangents.Length; i++)
            {
                Vector3 tan = Quaternion.Euler(0, 0, -90) * normals[i];
                tangents[i] = new Vector4(tan.x, tan.y, tan.z, -1);
            }
            return tangents;
        }

        /// <summary>
        /// Calculate bounds based on verticies.
        /// </summary>
        /// <returns>Bounds based on verticies.</returns>
        public static Bounds GetBounds(Vector3[] verticies)
        {
            Vector3 maxBounds = new Vector3();
            Vector3 minBounds = new Vector3();

            for (int i = 0; i < verticies.Length; i++)
            {
                if (verticies[i].x > maxBounds.x)
                {
                    maxBounds.x = verticies[i].x;
                }
                if (verticies[i].y > maxBounds.y)
                {
                    maxBounds.y = verticies[i].y;
                }
                if (verticies[i].z > maxBounds.z)
                {
                    maxBounds.z = verticies[i].z;
                }

                if (verticies[i].x < minBounds.x)
                {
                    minBounds.x = verticies[i].x;
                }
                if (verticies[i].y < minBounds.y)
                {
                    minBounds.y = verticies[i].y;
                }
                if (verticies[i].z < minBounds.z)
                {
                    minBounds.z = verticies[i].z;
                }
            }

            Vector3 boundsSize = maxBounds - minBounds;
            Vector3 boundsCenter = (maxBounds + minBounds) / 2;
            return new Bounds(boundsCenter, boundsSize);
        }

        /// <summary>
        /// Calculate normals array.
        /// </summary>
        /// <returns>Calculated Vector3 array of normals.</returns>
        public static Vector3[] GetNormals(Vector3[] verticies, int[] triangles)
        {
            Vector3[] vertexNormals = new Vector3[verticies.Length];
            int triangleCount = triangles.Length / 3;
            for (int i = 0; i < triangleCount; i++)
            {
                int normalTriangleIndex = i * 3;
                int vertexIndexA = triangles[normalTriangleIndex];
                int vertexIndexB = triangles[normalTriangleIndex + 1];
                int vertexIndexC = triangles[normalTriangleIndex + 2];

                Vector3 triangleNormal = NormalFromIndices(verticies, vertexIndexA, vertexIndexB, vertexIndexC);
                vertexNormals[vertexIndexA] += triangleNormal;
                vertexNormals[vertexIndexB] += triangleNormal;
                vertexNormals[vertexIndexC] += triangleNormal;
            }

            for (int i = 0; i < vertexNormals.Length; i++)
            {
                vertexNormals[i].Normalize();
            }

            return vertexNormals;
        }

        /// <summary>
        /// Calculate one normal from indices.
        /// </summary>
        /// <returns>Calculated one normal from indices.</returns>
        private static Vector3 NormalFromIndices(Vector3[] verticies, int indexA, int indexB, int indexC)
        {
            Vector3 pointA = verticies[indexA];
            Vector3 pointB = verticies[indexB];
            Vector3 pointC = verticies[indexC];

            Vector3 sideAB = pointB - pointA;
            Vector3 sideAC = pointC - pointA;
            return Vector3.Cross(sideAB, sideAC).normalized;
        }

        public MeshVertexData[] Verticies;
        public int[] Triangles;
        public Bounds Bounds;
    }
}
