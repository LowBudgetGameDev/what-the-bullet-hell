using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;

    private HealthSystem bossHealth;

    private void Start()
    {
        BossManager.Instance.OnBossSpawned += (object sender, BossManager.OnBossSpawnedEventArgs e) =>
        {
            bossHealth = e.bossTransform.GetComponent<HealthSystem>();

            bossHealth.OnHealthChanged += BossUI_OnHealthChanged;
        };
    }

    private void BossUI_OnHealthChanged(object sender, System.EventArgs e)
    {
        healthBar.localScale = new Vector3(bossHealth.GetHealthNormalized(), 1, 1);
    }
}
