using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private IBossAttack[] attackList;

    private int collisionDamage = 1;

    public void SetUp()
    {
        attackList = GetComponents<IBossAttack>();
        FunctionTimer.Create(() => { attackList[0].Attack(); }, 1f);
    }

    private void Update()
    {
        
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
