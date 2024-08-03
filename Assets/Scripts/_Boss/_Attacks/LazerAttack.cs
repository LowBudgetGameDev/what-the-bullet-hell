using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : MonoBehaviour, IBossAttack
{
    private Transform lazer;
    private List<Transform> lazerInstances;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        lazer = Resources.Load<Transform>("Lazer");
        rigidbody2D = GetComponent<Rigidbody2D>();
        lazerInstances = new List<Transform>();
    }

    public void Attack()
    {
        int numLazers = 4;
        for (int i = 0; i < numLazers; i++)
        {
            float angle = 360 / numLazers * i;
            lazerInstances.Add(Instantiate(lazer, Vector3.zero, Quaternion.Euler(0, 0, angle), transform));
        }

        float spinSpeed = 18;
        rigidbody2D.angularVelocity = spinSpeed;

        RepeatedFunctionTimer soundMaker = RepeatedFunctionTimer.Create(
            () =>
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.Boss_Lazer, 0.95f);
            }, 2f, 15f);
    }

    public void BetterAttack()
    {
        int numLazers = 4;
        for (int i = 0; i < numLazers; i++)
        {
            float angle = 360 / numLazers * i;
            lazerInstances.Add(Instantiate(lazer, Vector3.zero, Quaternion.Euler(0, 0, angle), transform));
        }

        float spinSpeed = 27;
        rigidbody2D.angularVelocity = spinSpeed;

        RepeatedFunctionTimer soundMaker = RepeatedFunctionTimer.Create(
            () =>
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.Boss_Lazer, 0.95f);
            }, 2f, 15f);
    }

    public void StopAttack()
    {
        foreach(Transform lazer in lazerInstances)
        {
            Destroy(lazer.gameObject);
        }

        lazerInstances = new List<Transform>();

        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.rotation = 0f;
    }
}
