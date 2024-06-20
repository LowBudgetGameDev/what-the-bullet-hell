using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Transform damageParticles;

    private void Start()
    {
        GetComponent<HealthSystem>().OnDamaged += EnemyDamage_OnDamaged;
    }

    private void EnemyDamage_OnDamaged(object sender, System.EventArgs e)
    {
        Instantiate(damageParticles, transform.position, Quaternion.identity);
    }
}
