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
        if (e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun))
        {
            gameObject.AddComponent(e.upgrade.GetScriptType());
        }
    }
}
