using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public event EventHandler OnLevelUp;

    private int level;
    private int xp;
    private int startLevelUpXp = 5;
    private int levelUpXp;
    private int levelUpXpIncrease = 3;

    private void Awake()
    {
        Instance = this;

        level = 0;
        xp = 0;
        levelUpXp = startLevelUpXp;
    }

    public void GainXp(int amount)
    {
        xp += amount;

        if (xp >= levelUpXp) LevelUp();
    }

    private void LevelUp()
    {
        level++;
        xp -= levelUpXp;

        levelUpXp += levelUpXpIncrease;

        OnLevelUp?.Invoke(this, EventArgs.Empty);
        Debug.Log(level);
    }
}
