using System.Threading;
using AdvancedMeshCreator;
using UnityEngine;
using Unity.Mathematics;

public class Demo : MonoBehaviour
{
    private bool NeedUpdate;

    private MeshData MeshData;

    private void Start()
    {
        Thread thread = new Thread(() =>
        {
            const int pointsInRow = 64;
            const float pointSize = 0.5f;

            Vector3[] vertices = new Vector3[pointsInRow * pointsInRow];
            int[] triangles = new int[pointsInRow * pointsInRow * 6];

            for (int i = 0, x = 0; x < pointsInRow; x++)
            {
                for (int z = 0; z < pointsInRow; z++)
                {
                    vertices[i] = new Vector3(x * pointSize, noise.cnoise(new float2(x, z) / 20) * 5, z * pointSize);

                    i++;
                }
            }

            for (int vert = 0, tris = 0, x = 0; x < pointsInRow - 1; x++)
            {
                for (int y = 0; y < pointsInRow - 1; y++)
                {
                    triangles[tris + 0] = vert;
                    triangles[tris + 1] = vert + 1;
                    triangles[tris + 2] = pointsInRow + vert;
                    triangles[tris + 3] = pointsInRow + vert;
                    triangles[tris + 4] = vert + 1;
                    triangles[tris + 5] = pointsInRow + vert + 1;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            MeshData = new MeshData(vertices, triangles); // Calculate Mesh

            NeedUpdate = true;
        });
        thread.IsBackground = true;
        thread.Start();
    }

    private void Update()
    {
        if (NeedUpdate)
        {
            NeedUpdate = false;

            CreateMesh();
        }
    }

    private void CreateMesh()
    {
        GetComponent<MeshFilter>().mesh = MeshCreator.CreateMesh(MeshData);
    }
}
