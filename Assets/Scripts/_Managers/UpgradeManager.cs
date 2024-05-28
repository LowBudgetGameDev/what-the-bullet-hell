using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum Upgrade
    {
        Health,
        Fire_Rate,
        Life_Steal,
        Explosive_Bullets,
        Poison,
        Piercing_Bullets,
        Large_Bullets,
        Shotgun
    }

    public class UpgradeUnlockedEventArgs : EventArgs
    {
        public UpgradeSO upgrade;
    }

    public static UpgradeManager Instance { get; private set; }

    public event EventHandler OnUpgradeLevelUp;
    public event EventHandler<UpgradeUnlockedEventArgs> OnUpgradeUnlocked;

    private Dictionary<Upgrade, UpgradeSO> upgradeSOConversion;

    private Dictionary<UpgradeSO, int> upgradeLevelDictionary;
    private Dictionary<UpgradeSO, int> counterLevelDictionary;

    private List<UpgradeSO> unlockedUpgrades;
    private List<UpgradeSO> unlockedCounters;

    private int maxUnlockableUpgrades = 3;
    private int maxUpgradeLevel = 5;

    private void Awake()
    {
        Instance = this;

        upgradeSOConversion = new Dictionary<Upgrade, UpgradeSO>();

        UpgradeListSO upgradeListSO = Resources.Load<UpgradeListSO>(typeof(UpgradeListSO).ToString());

        foreach (UpgradeSO upgrade in upgradeListSO.list)
        {
            upgradeSOConversion.Add(upgrade.upgrade, upgrade);
        }

        upgradeLevelDictionary = new Dictionary<UpgradeSO, int>();
        counterLevelDictionary = new Dictionary<UpgradeSO, int>();

        unlockedUpgrades = new List<UpgradeSO>();
        unlockedCounters = new List<UpgradeSO>();
    }

    public void LevelUpUpgrade(UpgradeSO upgrade)
    {
        if (!upgradeLevelDictionary.ContainsKey(upgrade))
        {
            upgradeLevelDictionary.Add(upgrade, 0);
            counterLevelDictionary.Add(upgrade.counter, 0);
            unlockedUpgrades.Add(upgrade);
            unlockedCounters.Add(upgrade.counter);

            OnUpgradeUnlocked?.Invoke(this, new UpgradeUnlockedEventArgs { upgrade = upgrade });
        }

        if (upgradeLevelDictionary[upgrade] == maxUpgradeLevel) return;

        upgradeLevelDictionary[upgrade]++;
        counterLevelDictionary[upgrade.counter]++;

        OnUpgradeLevelUp?.Invoke(this, EventArgs.Empty);
    }

    public int GetLevelOfUpgrade(UpgradeSO upgrade)
    {
        if (!upgradeLevelDictionary.ContainsKey(upgrade))
        {
            return GetLevelOfCounter(upgrade);
        }

        return upgradeLevelDictionary[upgrade];
    }

    public int GetLevelOfCounter(UpgradeSO counter)
    {
        if (!counterLevelDictionary.ContainsKey(counter))
        {
            return 0;
        }

        return counterLevelDictionary[counter];
    }

    public UpgradeSO GetUpgradeSO(Upgrade upgrade)
    {
        return upgradeSOConversion[upgrade];
    }

    public List<UpgradeSO> GetUnlockedUpgrades()
    {
        return unlockedUpgrades;
    }

    public bool HasUnlockedUpgrade(UpgradeSO upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }

    public bool CanUnlockUpgrades()
    {
        return upgradeLevelDictionary.Count < maxUnlockableUpgrades;
    }

    public bool CanLevelUpUpgrade(UpgradeSO upgrade)
    {
        return GetLevelOfUpgrade(upgrade) < maxUpgradeLevel;
    }
}
