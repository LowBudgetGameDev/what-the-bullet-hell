using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TrailRenderer trail;

    private Color originalColor;

    private void Awake()
    {
        originalColor = sprite.color;
    }

    public void UpdateBulletColor(Transform shooter)
    {
        IUpgrade[] upgrades = shooter.GetComponents<IUpgrade>();

        Color bulletColor = originalColor;

        foreach (IUpgrade upgrade in upgrades)
        {
            if (upgrade.GetUpgrade().color == new Color(1, 1, 1)) continue;

            bulletColor = Color.Lerp(bulletColor, upgrade.GetUpgrade().color, 0.5f);
        }

        sprite.color = bulletColor;
        trail.startColor = bulletColor;
        trail.endColor = bulletColor;
    }
}
