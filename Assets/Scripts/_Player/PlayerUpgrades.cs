using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += PlayerUpgrades_OnUpgradeUnlocked;
    }

    private void PlayerUpgrades_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        if (e.upgrade == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Health) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Fire_Rate) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Large_Bullets) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Shotgun))
        {
            gameObject.AddComponent(e.upgrade.GetScriptType());
        }
    }
}
