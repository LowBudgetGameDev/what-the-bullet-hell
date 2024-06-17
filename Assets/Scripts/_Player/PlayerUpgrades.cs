using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += PlayerUpgrades_OnUpgradeUnlocked;

        UpgradeManager.Instance.OnUpgradeLevelUp += PlayerUpgrades_OnUpgradeLevelUp;
    }

    private void PlayerUpgrades_OnUpgradeLevelUp(object sender, System.EventArgs e)
    {
        foreach (IUpgrade upgrade in GetComponents<IUpgrade>())
        {
            upgrade.SetIsCounter(false);
            upgrade.OnAdded();
        }
    }

    private void PlayerUpgrades_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        gameObject.AddComponent(e.upgrade.GetScriptType());
    }
}
