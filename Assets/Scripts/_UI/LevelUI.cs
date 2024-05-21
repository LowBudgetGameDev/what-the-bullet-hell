using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        LevelManager.Instance.OnXpIncreased += (object sender, EventArgs e) =>
        {
            UpdateText();
        };

        LevelManager.Instance.OnLevelUp += (object sender, EventArgs e) =>
        {
            UpdateText();
        };

        UpdateText();
    }

    private void UpdateText()
    {
        xpText.text = LevelManager.Instance.GetXp().ToString() + " XP / " + LevelManager.Instance.GetLevelUpXpAmount().ToString() + " XP";
        levelText.text = "LVL. " + LevelManager.Instance.GetLevel().ToString();
    }
}
