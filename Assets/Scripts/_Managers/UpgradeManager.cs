using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public class UpgradeUnlockedEventArgs : EventArgs
    {
        public UpgradeSO upgrade;
    }

    public static UpgradeManager Instance { get; private set; }

    public event EventHandler OnUpgradeLevelUp;
    public event EventHandler<UpgradeUnlockedEventArgs> OnUpgradeUnlocked;

    private Dictionary<Upgrade, UpgradeSO> upgradeSOConversion;

    private List<UpgradeSO> unlockedUpgrades;
    private List<UpgradeSO> unlockedCounters;

    private int maxUnlockableUpgrades = 3;

    private void Awake()
    {
        Instance = this;

        upgradeSOConversion = new Dictionary<Upgrade, UpgradeSO>();

        UpgradeListSO upgradeListSO = Resources.Load<UpgradeListSO>(typeof(UpgradeListSO).ToString());

        foreach (UpgradeSO upgrade in upgradeListSO.list)
        {
            upgradeSOConversion.Add(upgrade.upgrade, upgrade);
            upgrade.CreateUpgradeClass();
        }

        unlockedUpgrades = new List<UpgradeSO>();
        unlockedCounters = new List<UpgradeSO>();
    }

    public void LevelUpUpgrade(UpgradeSO upgrade)
    {
        if (!unlockedUpgrades.Contains(upgrade))
        {
            unlockedUpgrades.Add(upgrade);
            unlockedCounters.Add(upgrade.counter);

            OnUpgradeUnlocked?.Invoke(this, new UpgradeUnlockedEventArgs { upgrade = upgrade });
        }

        upgrade.LevelUp();
        upgrade.counter.CounterLevelUp();

        OnUpgradeLevelUp?.Invoke(this, EventArgs.Empty);
    }

    public UpgradeSO GetUpgradeSO(Upgrade upgrade)
    {
        return upgradeSOConversion[upgrade];
    }

    public List<UpgradeSO> GetUnlockedUpgrades()
    {
        return unlockedUpgrades;
    }

    public List<UpgradeSO> GetUnlockedCounters()
    {
        return unlockedCounters;
    }

    public bool HasUnlockedUpgrade(UpgradeSO upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }

    public bool CanUnlockUpgrades()
    {
        return unlockedUpgrades.Count < maxUnlockableUpgrades;
    }
}
