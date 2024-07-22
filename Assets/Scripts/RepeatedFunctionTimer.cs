using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedFunctionTimer
{
    public static RepeatedFunctionTimer Create(Action action, float interval, float timer)
    {
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonobehaviorHook));

        RepeatedFunctionTimer functionTimer = new RepeatedFunctionTimer(action, interval, timer, gameObject);

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
    private float interval;
    private float intervalTimer;
    private float timer;
    private GameObject gameObject;
    private bool isDestroyed;

    private RepeatedFunctionTimer(Action action, float interval, float timer, GameObject gameObject)
    {
        this.action = action;
        this.interval = interval;
        this.timer = timer;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if (isDestroyed) return;

        timer -= Time.unscaledDeltaTime;
        intervalTimer -= Time.unscaledDeltaTime;

        if (intervalTimer < 0f)
        {
            action();
            intervalTimer += interval;
        }

        if (timer < 0f)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}
