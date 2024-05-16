using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private int damageAmount = 1;

    private void Start()
    {
        GetComponent<PlayerController>().OnShoot += PlayerShoot_OnShoot;
    }

    private void PlayerShoot_OnShoot(object sender, System.EventArgs e)
    {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

        Vector3 dirToMouse = (mouseWorldPosition - transform.position).normalized;

        bullet.GetComponent<Bullet>().SetUp(dirToMouse, damageAmount);
    }
}
