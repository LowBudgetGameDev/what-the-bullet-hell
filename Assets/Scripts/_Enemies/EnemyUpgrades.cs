using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpgrades : MonoBehaviour
{
    private void Start()
    {
        List<UpgradeSO> counters = UpgradeManager.Instance.GetUnlockedCounters();

        foreach (UpgradeSO counter in counters)
        {
            gameObject.AddComponent(counter.GetScriptType());
        }

        foreach (IUpgrade upgrade in GetComponents<IUpgrade>())
        {
            upgrade.SetIsCounter(true);
            upgrade.OnAdded();
        }
    }
}
