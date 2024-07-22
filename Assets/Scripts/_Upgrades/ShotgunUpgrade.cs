using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUpgrade : MonoBehaviour, IUpgrade
{
    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun);
    }

    public int GetBulletAmount()
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun);

        return (int) upgrade.GetUpgradeAmount(isCounter);
    }

    public void OnShoot(Transform bullet)
    {
        //bullet.localScale = new Vector3(-upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f, -upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f, -upgrade.GetUpgradeAmount(isCounter) / 2 + 0.5f);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun);

        if (transform == null) return;

        if (!isCounter)
        {
            GetComponent<PlayerShoot>().SetBulletsPerShot((int) upgrade.GetUpgradeAmount());
        }
        else
        {
            GetComponent<EnemyShoot>()?.SetBulletsPerShot((int) upgrade.GetCounterAmount());
        }
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
