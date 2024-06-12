using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer CreateFunctionTimer(Action action, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonobehaviorHook));

        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);

        gameObject.GetComponent<MonobehaviorHook>().onUpdate = functionTimer.Update;

        return functionTimer;
    }

    private class MonobehaviorHook : MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            onUpdate?.Invoke();
        }
    }

    private Action action;
    private float timer;
    private GameObject gameObject;
    private bool isDestroyed;

    private FunctionTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if (isDestroyed) return;

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            action();
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}
