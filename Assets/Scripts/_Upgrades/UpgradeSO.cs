using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public UpgradeManager.Upgrade upgrade;
    public float levelUpAmount;
    public string scriptName;
    public UpgradeSO counter;
    public Sprite icon;
    public string nameString;
    public string description;

    public Type GetScriptType()
    {
        return Type.GetType(scriptName);
    }
}
