using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletUpgrade : MonoBehaviour
{
    public abstract void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy);
}
