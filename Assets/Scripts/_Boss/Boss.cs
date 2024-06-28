using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private IBossAttack[] attackList;

    public void SetUp()
    {
        attackList = GetComponents<IBossAttack>();
    }
}
