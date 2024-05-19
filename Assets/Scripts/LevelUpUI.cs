using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        LevelManager.Instance.OnLevelUp += (object sender, EventArgs e) => { Show(); };

        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
        Time.timeScale = 0f;
    }

    private void Hide()
    {
        animator.SetTrigger("Close");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
