using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMeshCollider : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    private Mesh mesh;
    private MeshCollider meshCollider;

    private void Start()
    {
        mesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        UpdateMesh();
        meshCollider.sharedMesh = null; // Clear the current mesh collider
        meshCollider.sharedMesh = mesh; // Assign the updated mesh
    }

    private void UpdateMesh()
    {
        int positionCount = trail.positionCount;
        if (positionCount < 2)
        {
            mesh.Clear();
            return;
        }

        Vector3[] positions = new Vector3[positionCount];
        trail.GetPositions(positions);

        Vector3[] vertices = new Vector3[positionCount * 2];
        int[] triangles = new int[(positionCount - 1) * 6];
        Vector2[] uvs = new Vector2[positionCount * 2];

        for (int i = 0; i < positionCount; i++)
        {
            Vector3 pos = positions[i];
            float width = Mathf.Lerp(trail.startWidth, trail.endWidth, (float)i / (positionCount - 1));

            // Correct the scale issue by accounting for the parent's scale
            Vector3 scale = transform.parent.localScale;
            width /= Mathf.Max(scale.x, scale.y);

            // Adjust the normal calculation to face the opposite direction
            Vector3 normal = Vector3.Cross(pos, Vector3.forward).normalized;
            vertices[i * 2] = pos + normal * width / 2;
            vertices[i * 2 + 1] = pos - normal * width / 2;

            uvs[i * 2] = new Vector2((float)i / (positionCount - 1), 0);
            uvs[i * 2 + 1] = new Vector2((float)i / (positionCount - 1), 1);

            if (i < positionCount - 1)
            {
                int baseIndex = i * 2;
                int nextBaseIndex = (i + 1) * 2;

                triangles[i * 6] = baseIndex;
                triangles[i * 6 + 1] = nextBaseIndex;
                triangles[i * 6 + 2] = baseIndex + 1;

                triangles[i * 6 + 3] = baseIndex + 1;
                triangles[i * 6 + 4] = nextBaseIndex;
                triangles[i * 6 + 5] = nextBaseIndex + 1;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
