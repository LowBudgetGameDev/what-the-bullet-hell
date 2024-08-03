using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAttack : MonoBehaviour, IBossAttack
{
    private Transform ballPrefab;

    private List<Transform> healingBallInstances;

    private void Awake()
    {
        ballPrefab = Resources.Load<Transform>("HealingBall");

        healingBallInstances = new List<Transform>();
    }

    public void Attack()
    {
        Transform healingBall1 = Instantiate(ballPrefab, new Vector3(30f, 15f), Quaternion.identity);
        healingBall1.GetComponent<HealingBall>().Setup(transform, 6250);
        healingBallInstances.Add(healingBall1);

        Transform healingBall2 = Instantiate(ballPrefab, new Vector3(-30f, 15f), Quaternion.identity);
        healingBall2.GetComponent<HealingBall>().Setup(transform, 6250);
        healingBallInstances.Add(healingBall2);

        Transform healingBall3 = Instantiate(ballPrefab, new Vector3(30f, -15f), Quaternion.identity);
        healingBall3.GetComponent<HealingBall>().Setup(transform, 6250);
        healingBallInstances.Add(healingBall3);

        Transform healingBall4 = Instantiate(ballPrefab, new Vector3(-30f, -15f), Quaternion.identity);
        healingBall4.GetComponent<HealingBall>().Setup(transform, 6250);
        healingBallInstances.Add(healingBall4);

        RepeatedFunctionTimer soundMaker = RepeatedFunctionTimer.Create(
            () =>
            {
                if (healingBall1 == null && healingBall2 == null && healingBall3 == null && healingBall4 == null) return;

                SoundManager.Instance.PlaySound(SoundManager.Sound.Boss_Heal);
            },1f, 15f);
    }

    public void StopAttack()
    {
        foreach (Transform healingBall in healingBallInstances)
        {
            if (healingBall != null) Destroy(healingBall.gameObject);
        }

        healingBallInstances = new List<Transform>();
    }
}
