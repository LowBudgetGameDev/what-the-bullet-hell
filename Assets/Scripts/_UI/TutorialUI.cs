using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button continueButton1;
    [SerializeField] private Button continueButton2;
    [SerializeField] private Button continueButton3;

    private float slideTimer;
    private float slideTarget;
    private float slideStart;
    private bool isSliding;

    private void Awake()
    {
        tutorialButton.onClick.AddListener(() =>
        {
            slideTimer = 0f;
            slideStart = 1280f;
            slideTarget = 0f;
            isSliding = true;
        });

        continueButton1.onClick.AddListener(() =>
        {
            slideTimer = 0f;
            slideStart = 0f;
            slideTarget = -1280f;
            isSliding = true;
        });

        continueButton2.onClick.AddListener(() =>
        {
            slideTimer = 0f;
            slideStart = -1280f;
            slideTarget = -2560f;
            isSliding = true;
        });

        continueButton3.onClick.AddListener(() =>
        {
            slideTimer = 0f;
            slideStart = -2560f;
            slideTarget = -3840f;
            isSliding = true;
        });
    }

    private void Update()
    {
        if (!isSliding) return;

        slideTimer += Time.deltaTime;
        slideTimer = Mathf.Clamp01(slideTimer);

        float xPos = Mathf.Lerp(slideStart, slideTarget, slideTimer);

        if (xPos == -3840f) xPos = 1280f;

        transform.position = new Vector3(xPos / 30f, 0f, 0f);

        if (slideTimer == 1f) isSliding = false;
    }
}
