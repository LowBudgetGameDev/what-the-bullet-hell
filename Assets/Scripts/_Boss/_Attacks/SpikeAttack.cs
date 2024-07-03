using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : MonoBehaviour, IBossAttack
{
    private Transform spikes;
    private Transform spikesInstance;

    private void Awake()
    {
        spikes = Resources.Load<Transform>("Spikes");
    }

    public void Attack()
    {
        spikesInstance = Instantiate(spikes, transform);
    }

    public void StopAttack()
    {
        Destroy(spikesInstance.gameObject);
    }
}
