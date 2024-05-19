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

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetUp(UpgradeSO upgrade)
    {
        nameText.text = upgrade.nameString;
        description.text = upgrade.description;
        counterText.text = "The Enemies Will Gain " + upgrade.counter.nameString;

        button.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.LevelUpUpgrade(upgrade);

            OnClick?.Invoke(this, EventArgs.Empty);
        });
    }
}
