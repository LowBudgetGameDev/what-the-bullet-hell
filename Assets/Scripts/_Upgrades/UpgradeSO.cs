using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    private class UpgradeClass
    {
        public int Level { get; private set; }
        public int CounterLevel { get; private set; }
        private int maxLevel = 5;

        public void IncreaseLevel()
        {
            if (Level == maxLevel) return;

            Level++;
        }

        public void IncreaseCounterLevel()
        {
            if (CounterLevel == maxLevel) return;

            CounterLevel++;
        }

        public bool IsMaxLevel()
        {
            return Level == maxLevel;
        }

        public bool IsMaxCounterLevel()
        {
            return CounterLevel == maxLevel;
        }
    }

    public Upgrade upgrade;
    public float startAmount;
    public float levelUpAmount;
    public string scriptName;
    public UpgradeSO counter;
    public Sprite icon;
    public string nameString;
    public string description;

    private UpgradeClass upgradeClass = new UpgradeClass();

    public void CreateUpgradeClass()
    {
        upgradeClass = new UpgradeClass();
    }

    public Type GetScriptType()
    {
        return Type.GetType(scriptName);
    }

    public int GetLevel()
    {
        return upgradeClass.Level;
    }

    public int GetCounterLevel()
    {
        return upgradeClass.CounterLevel;
    }

    public int GetLevel(bool isCounter)
    {
        return !isCounter ? GetLevel() : GetCounterLevel();
    }

    public void LevelUp()
    {
        upgradeClass.IncreaseLevel();
    }

    public void CounterLevelUp()
    {
        upgradeClass.IncreaseCounterLevel();
    }

    public bool IsMaxLevel()
    {
        return upgradeClass.IsMaxLevel();
    }

    public bool IsMaxCounterLevel()
    {
        return upgradeClass.IsMaxCounterLevel();
    }

    public bool IsMaxLevel(bool isCounter)
    {
        return !isCounter ? IsMaxLevel() : IsMaxCounterLevel();
    }

    public float GetUpgradeAmount()
    {
        return startAmount + upgradeClass.Level * levelUpAmount;
    }

    public float GetCounterAmount()
    {
        return startAmount + upgradeClass.CounterLevel * levelUpAmount;
    }

    public float GetUpgradeAmount(bool isCounter)
    {
        return !isCounter ? GetUpgradeAmount() : GetCounterAmount();
    }
}

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
