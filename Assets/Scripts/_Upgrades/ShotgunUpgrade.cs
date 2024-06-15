using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun);
    }

    public int GetBulletAmount()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun);

        return (int) (upgrade.GetLevel() * upgrade.levelUpAmount);
    }
}
