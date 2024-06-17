using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private int damageAmount = 1;
    private int bulletsPerShot;

    private void Awake()
    {
        bulletsPerShot = 1;
    }

    private void Start()
    {
        GetComponent<PlayerController>().OnShoot += PlayerShoot_OnShoot;
    }

    public void SetBulletsPerShot(int amount)
    {
        bulletsPerShot = amount;
    }

    private void PlayerShoot_OnShoot(object sender, System.EventArgs e)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Transform bullet = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, transform.position, Quaternion.identity);

            foreach (IUpgrade upgrade in GetComponents<IUpgrade>())
            {
                upgrade.OnShoot(bullet);
            }

            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            Vector3 dirToMouse = (mouseWorldPosition - transform.position).normalized;

            if (bulletsPerShot == 1)
            {
                bullet.GetComponent<Bullet>().SetUp(dirToMouse, damageAmount, transform);
                return;
            }

            float shootAngle = UtilsClass.VectorToAngleDegrees(dirToMouse);

            bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(shootAngle + i * 120 / (bulletsPerShot - 1) - 60), damageAmount, transform);
        }
    }
}
