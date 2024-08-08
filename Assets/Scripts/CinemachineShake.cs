using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;

    private CinemachineVirtualCamera virtialCamera;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;

        virtialCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float amplitude, float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtialCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer < 0f) return;

        shakeTimer -= Time.deltaTime;
        if (shakeTimer < 0f)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtialCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}
