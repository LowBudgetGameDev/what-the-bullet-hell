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
        bullet.localScale *= upgrade.GetUpgradeAmount(isCounter);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets);
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
