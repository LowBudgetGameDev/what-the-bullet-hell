using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthRing;

    private HealthSystem healthSystem;
    private Animator healthAnimator;

    private float updateRingTimerMax = 0.5f;
    private float updateRingTimer;

    private float oldScale;
    private float newScale;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthSystem.OnHealthChanged += (object sender, EventArgs e) => { healthAnimator.SetTrigger("TookDamage"); };
        LevelManager.Instance.OnLevelUp += (object sender, EventArgs e) => { healthAnimator.SetTrigger("TookDamage"); };
    }

    private void Update()
    {
        if (updateRingTimer >= updateRingTimerMax) return;

        updateRingTimer += Time.deltaTime;

        if (updateRingTimer > updateRingTimerMax)
        {
            healthRing.fillAmount = newScale;
            return;
        }

        float timerNormalized = updateRingTimer / updateRingTimerMax;

        healthRing.fillAmount = Mathf.Lerp(oldScale, newScale, timerNormalized);
    }

    public void UpdateHealthRing()
    {
        updateRingTimer = 0f;

        oldScale = healthRing.fillAmount;
        newScale = healthSystem.GetHealthNormalized();
    }
}
