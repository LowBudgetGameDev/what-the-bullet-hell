using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health);
    }

    public int GetExtraHealthAmount()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health);

        return (int) (upgrade.GetLevel() * upgrade.levelUpAmount);
    }
}
