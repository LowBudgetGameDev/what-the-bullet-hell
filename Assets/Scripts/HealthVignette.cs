using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthVignette : MonoBehaviour
{
    private Volume postProcessingVolume;

    private HealthSystem playerHealth;

    private float maxVignetteStrength = 0.45f;

    private void Awake()
    {
        postProcessingVolume = GetComponent<Volume>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();

        playerHealth.OnHealthChanged += HealthVignette_OnHealthChanged;
    }

    private void HealthVignette_OnHealthChanged(object sender, System.EventArgs e)
    {
        float strength = -(playerHealth.GetHealthNormalized() - 0.5f) * 2f;

        strength = Mathf.Clamp01(strength);

        SetVignetteStrength(strength);
    }

    private void SetVignetteStrength(float strengthNormalized)
    {
        if (postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.Override(strengthNormalized * maxVignetteStrength);
        }
    }
}
