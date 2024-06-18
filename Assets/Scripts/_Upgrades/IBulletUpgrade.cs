using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletUpgrade
{
    public void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy);
}
