using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Shotgun);
    }

    public int GetBulletAmount()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Shotgun);

        return (int) (UpgradeManager.Instance.GetLevelOfUpgrade(upgrade) * upgrade.levelUpAmount);
    }
}
