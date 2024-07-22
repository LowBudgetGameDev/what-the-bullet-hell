using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour, IUpgrade
{
    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health);
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
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health);

        if (transform == null) return;

        GetComponent<HealthSystem>().AddBonusHealth((int)upgrade.GetUpgradeAmount(isCounter));
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
