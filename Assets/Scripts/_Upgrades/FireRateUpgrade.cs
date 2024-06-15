using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpgrade : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate);
    }

    public float GetFireRateMultiplier()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate);

        return 1f - upgrade.GetLevel() * upgrade.levelUpAmount;
    }
}
