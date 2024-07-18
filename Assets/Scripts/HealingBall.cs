using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBall : MonoBehaviour
{
    private Transform healingTarget;
    private int hpPerSecond;

    private float healingTimer;

    public void Setup(Transform healingTarget, int hpPerSecond)
    {
        this.healingTarget = healingTarget;
        this.hpPerSecond = hpPerSecond;
    }

    private void Update()
    {
        healingTimer -= Time.deltaTime;

        if (healingTimer < 0f)
        {
            HealTarget();
            healingTimer += 1f;
        }
    }

    private void HealTarget()
    {
        healingTarget.GetComponent<HealthSystem>().Heal(hpPerSecond);
    }
}
