using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthVignette : MonoBehaviour
{
    private static Volume postProcessingVolume;

    private static float maxVignetteStrength = 0.45f;

    private void Awake()
    {
        postProcessingVolume = GetComponent<Volume>();
    }

    public static void SetVignetteStrength(float strengthNormalized)
    {
        if (postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(strengthNormalized * maxVignetteStrength);
        }
    }
}
