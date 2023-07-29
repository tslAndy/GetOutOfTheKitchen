using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class BurstGun : Weapon
{
    [SerializeField] private int projectilesInBurst;
    [SerializeField] private float timeBetweenShots, timeBetweenBursts, speed;
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private Projectile projectilePrefab;

    private bool _canShoot = true;

    private IEnumerator BurstCoroutine()
    {
        _canShoot = false;
        for (int i = 0; i < projectilesInBurst; i++) 
        {
            SpawnProjectile();
            yield return new WaitForSeconds(timeBetweenShots);
        }
        yield return new WaitForSeconds(timeBetweenBursts);
        _canShoot = true;
    }

    private void SpawnProjectile()
    {
        Vector2 shootDirection = InputHandler.Instance.GetMouseDirection(projectileSpawnTransform);
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform);
        projectile.transform.SetParent(null);
        projectile.SetVelocity(shootDirection * speed);
    }

    public override void OnMainShootStarted()
    {
        if (_canShoot)
            StartCoroutine(BurstCoroutine());
    }
}
