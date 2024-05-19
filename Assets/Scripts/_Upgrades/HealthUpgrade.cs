using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Health);
    }

    public int GetExtraHealthAmount()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Health);

        return (int) (UpgradeManager.Instance.GetLevelOfUpgrade(upgrade) * upgrade.levelUpAmount);
    }
}
