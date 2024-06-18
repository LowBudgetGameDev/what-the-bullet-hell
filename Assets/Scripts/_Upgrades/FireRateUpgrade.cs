using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpgrade : MonoBehaviour, IUpgrade
{
    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate);
    }

    public float GetFireRateMultiplier()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate);

        return 1f - upgrade.GetLevel() * upgrade.levelUpAmount;
    }

    public void OnShoot(Transform bullet)
    {
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate);

        if (transform == null) return;

        if (!isCounter)
        {
            GetComponent<PlayerController>().DecreaseShootTimeMax(upgrade.GetUpgradeAmount());
        }
        else
        {
            GetComponent<EnemyShoot>().DecreaseShootTimeMax(upgrade.GetCounterAmount());
        }
    }
}
