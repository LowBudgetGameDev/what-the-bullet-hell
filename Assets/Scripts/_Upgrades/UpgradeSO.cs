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
        private int maxLevel = 5;

        public void IncreaseLevel()
        {
            if (Level == maxLevel) return;

            Level++;
        }

        public bool IsMaxLevel()
        {
            return Level == maxLevel;
        }
    }

    public Upgrade upgrade;
    public UpgradeType upgradeType;
    public float startAmount;
    public float levelUpAmount;
    public string scriptName;
    public UpgradeSO counter;
    public Sprite icon;
    public string nameString;
    public string description;

    private UpgradeClass upgradeClass = new UpgradeClass();

    public Type GetScriptType()
    {
        return Type.GetType(scriptName);
    }

    public int GetLevel()
    {
        return upgradeClass.Level;
    }

    public void LevelUp()
    {
        upgradeClass.IncreaseLevel();
    }

    public bool IsMaxLevel()
    {
        return upgradeClass.IsMaxLevel();
    }

    public float GetUpgradeAmount()
    {
        return startAmount + upgradeClass.Level * levelUpAmount;
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

public enum UpgradeType
{
    Shooter,
    Bullet
}
