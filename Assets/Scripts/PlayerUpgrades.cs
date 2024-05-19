using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerUpgrades : MonoBehaviour
{
    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += PlayerUpgrades_OnUpgradeUnlocked;
    }

    private void PlayerUpgrades_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        MonoScript upgradeScript = (MonoScript) e.upgrade.script;

        gameObject.AddComponent(upgradeScript.GetClass());
    }
}
