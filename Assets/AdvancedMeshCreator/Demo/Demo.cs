using System.Threading;
using AdvancedMeshCreator;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    private bool NeedUpdate;

    private MeshData MeshData;

    private string ProgressMessage;
    private int Progress;
    private int MaxProgress;
    private bool ShowProgress = true;

    public const int PointsInRow = 2048;
    public const float PointSize = 0.5f;

    [SerializeField] private Text ProgressText;
 
    private void Start()
    {
        Thread thread = new Thread(() =>
        {
            Vector3[] vertices = new Vector3[PointsInRow * PointsInRow];
            int[] triangles = new int[PointsInRow * PointsInRow * 6];

            ProgressMessage = "Generating vertices";
            MaxProgress = vertices.Length;

            for (int i = 0, x = 0; x < PointsInRow; x++)
            {
                for (int z = 0; z < PointsInRow; z++)
                {
                    vertices[i] = new Vector3(x * PointSize, noise.cnoise(new float2(x, z) / 20) * 5, z * PointSize);

                    Progress++;

                    i++;
                }
            }

            for (int vert = 0, tris = 0, x = 0; x < PointsInRow - 1; x++)
            {
                for (int y = 0; y < PointsInRow - 1; y++)
                {
                    triangles[tris + 0] = vert;
                    triangles[tris + 1] = vert + 1;
                    triangles[tris + 2] = PointsInRow + vert;
                    triangles[tris + 3] = PointsInRow + vert;
                    triangles[tris + 4] = vert + 1;
                    triangles[tris + 5] = PointsInRow + vert + 1;

                    Progress++;

                    vert++;
                    tris += 6;
                }

                vert++;
            }

            ShowProgress = false;
            ProgressMessage = "Calculating mesh";
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

            ProgressText.gameObject.SetActive(false);
        }

        if (ShowProgress)
        {
            ProgressText.text = $"{ProgressMessage} {Progress}/{MaxProgress}";
        }
        else
        {
            ProgressText.text = $"{ProgressMessage}";
        }
    }

    private void CreateMesh()
    {
        GetComponent<MeshFilter>().mesh = MeshCreator.CreateMesh(MeshData);
    }
}
