using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Weapons;

public class Shotgun : Weapon
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int bulletsInShot;
    [SerializeField] private float fireRate, angleBetweenBullets, speed;

    private bool _canShoot = true;

    public override void OnMainShootStarted()
    {
        if (_canShoot)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        Vector2 shootDirection = InputHandler.Instance.GetMouseDirection(projectileSpawnTransform);

        for (int i = 0; i < bulletsInShot; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform);
            projectile.transform.SetParent(null);
            float currentAngle = (bulletsInShot / 2 - i) * angleBetweenBullets;
            projectile.SetVelocity(shootDirection.Rotate(currentAngle) * speed);
        }

        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}
