using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Camera GetMainCamera()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        return mainCamera;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseWorldPosition = GetMainCamera().ScreenToWorldPoint(mousePosition);

        mouseWorldPosition.z = 0;

        return mouseWorldPosition;
    }

    public static float VectorToAngleDegrees(Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
