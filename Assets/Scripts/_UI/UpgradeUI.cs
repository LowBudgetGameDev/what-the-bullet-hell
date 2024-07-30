using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public event EventHandler OnClick;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private GameObject levelCounter;
    [SerializeField] private TextMeshProUGUI levelText;

    private Button button;

    public void SetUp(UpgradeSO upgrade)
    {
        button = GetComponent<Button>();

        nameText.text = upgrade.nameString;
        icon.sprite = upgrade.icon;
        description.text = upgrade.description;

        if (upgrade.GetCounter() != null)
        {
            counterText.text = "The Enemies Will Gain " + upgrade.GetCounter().nameString;
        } 
        else
        {
            counterText.text = "The Enemies Will Gain ??????????";
        }

        if (!UpgradeManager.Instance.HasUnlockedUpgrade(upgrade))
        {
            levelCounter.SetActive(false);
        }
        else
        {
            levelCounter.SetActive(true);
            levelText.text = upgrade.GetLevel().ToString();
        }

        button.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.LevelUpUpgrade(upgrade);

            OnClick?.Invoke(this, EventArgs.Empty);
        });

        if (upgrade.IsMaxLevel()) button.interactable = false;
    }
}
