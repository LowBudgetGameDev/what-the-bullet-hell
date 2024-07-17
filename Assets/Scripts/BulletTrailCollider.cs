using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailCollider : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    private PolygonCollider2D polygonCollider;

    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        int positionCount = trail.positionCount;
        if (positionCount < 2)
        {
            polygonCollider.pathCount = 0;
            return;
        }

        Vector3[] positions = new Vector3[positionCount];
        trail.GetPositions(positions);

        // Find the outermost points
        Vector3 start = positions[positionCount - 1];
        Vector3 end = positions[0];

        // Calculate direction from start to end
        Vector3 direction = (end - start).normalized;

        // Calculate perpendicular direction (flip the direction)
        Vector3 perpendicular = new Vector3(direction.y, -direction.x, 0f);

        // Determine width at start and end
        float startWidth = trail.startWidth;
        float endWidth = trail.endWidth;

        // Calculate collider points
        Vector2[] colliderPath = new Vector2[4];
        colliderPath[0] = transform.InverseTransformPoint(start + perpendicular * startWidth / 2f); // Start outer point
        colliderPath[1] = transform.InverseTransformPoint(start - perpendicular * startWidth / 2f); // Start inner point
        colliderPath[2] = transform.InverseTransformPoint(end - perpendicular * endWidth / 2f); // End inner point
        colliderPath[3] = transform.InverseTransformPoint(end + perpendicular * endWidth / 2f); // End outer point

        polygonCollider.SetPath(0, colliderPath);
    }
}
