using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private Transform upgradeContainer;
    [SerializeField] private Transform upgradeUIPrefab;

    private List<Transform> upgradeUIList;

    private Animator animator;

    private void Awake()
    {
        upgradeUIList = new List<Transform>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        LevelManager.Instance.OnLevelUp += (object sender, EventArgs e) => { Show(); };

        Hide();
    }

    private void OnEnable()
    {
        UpgradeManager.Upgrade[] possibleUpgrades = (UpgradeManager.Upgrade[])Enum.GetValues(typeof(UpgradeManager.Upgrade));

        if (!UpgradeManager.Instance.CanUnlockUpgrades())
        {
            for (int i = 0; i < 3; i++)
            {
                UpgradeSO upgradeSO = UpgradeManager.Instance.GetUnlockedUpgrades()[i];

                Transform upgradeUI = Instantiate(upgradeUIPrefab, upgradeContainer);

                upgradeUI.GetComponent<UpgradeUI>().SetUp(upgradeSO);
                upgradeUI.GetComponent<UpgradeUI>().OnClick += (object sender, EventArgs e) => { Hide(); };

                upgradeUIList.Add(upgradeUI);
            }
            return;
        }

        List<UpgradeManager.Upgrade> possibleUpgradesList = new List<UpgradeManager.Upgrade>(possibleUpgrades);

        for (int i = 0; i < 3; i++)
        {
            int upgradeIndex = Random.Range(0, possibleUpgradesList.Count);

            UpgradeSO upgradeSO = UpgradeManager.Instance.GetUpgradeSO(possibleUpgradesList[upgradeIndex]);

            Transform upgradeUI = Instantiate(upgradeUIPrefab, upgradeContainer);

            upgradeUI.GetComponent<UpgradeUI>().SetUp(upgradeSO);
            upgradeUI.GetComponent<UpgradeUI>().OnClick += (object sender, EventArgs e) => { Hide(); };

            upgradeUIList.Add(upgradeUI);

            possibleUpgradesList.RemoveAt(upgradeIndex);
        }
    }

    private void OnDisable()
    {
        foreach (Transform upgradeUI in upgradeUIList)
        {
            Destroy(upgradeUI.gameObject);
        }

        upgradeUIList.Clear();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        animator.SetTrigger("Close");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
