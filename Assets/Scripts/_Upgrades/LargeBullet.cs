using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBullet : MonoBehaviour
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Large_Bullets);
    }

    public float GetBulletScaledSize()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Large_Bullets);

        return UpgradeManager.Instance.GetLevelOfUpgrade(upgrade) * upgrade.levelUpAmount;
    }
}
