using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private IBossAttack[] attackList;

    private int collisionDamage = 1;

    private float attackDuration = 20f;
    private float restDuration = 5f;

    private float prevAttackIndex = -1;

    private bool isAttacking;
    private bool isResting;

    public void SetUp()
    {
        attackList = GetComponents<IBossAttack>();
        isResting = true;
        FunctionTimer.Create(() => { isResting = false; }, 1f);
    }

    private void Update()
    {
        if (isResting) return;

        if (isAttacking) return;

        int attackIndex = Random.Range(0, attackList.Length);

        while (attackIndex == prevAttackIndex) attackIndex = Random.Range(0, attackList.Length);

        IBossAttack attack = attackList[attackIndex];
        prevAttackIndex = attackIndex;

        attack.Attack();

        FunctionTimer.Create(() =>
        {
            attack.StopAttack();
            isResting = true;
            isAttacking = false;
            FunctionTimer.Create(() =>
            {
                isResting = false;

            }, restDuration);

        }, attackDuration);

        isAttacking = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<HealthSystem>().Damage(collisionDamage);

            float damageForce = 15f;
            float forceDuration = 0.5f;
            player.GetComponent<PlayerController>().ApplyForce((player.transform.position - transform.position).normalized * damageForce, ForceMode2D.Impulse, forceDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<HealthSystem>().Damage(collisionDamage);

            float damageForce = 15f;
            float forceDuration = 0.5f;
            player.GetComponent<PlayerController>().ApplyForce((player.transform.position - transform.position).normalized * damageForce, ForceMode2D.Impulse, forceDuration);
        }
    }
}
