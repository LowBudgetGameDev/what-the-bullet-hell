using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeBullet : MonoBehaviour, IUpgrade
{
    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets);
    }

    public void OnShoot(Transform bullet)
    {
        bullet.localScale = new Vector3(upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f, upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f, upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets);
    }
}
