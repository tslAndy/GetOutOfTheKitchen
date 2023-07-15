using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Super pistol")]
public class SuperPistol : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate, projectileSpeed;

    private float _lastShotTime;

    public override void Attack(Vector2 direction, Transform spawnTransform)
    {

        if (Time.time < _lastShotTime + fireRate)
            return;

        Projectile projectile = Instantiate(projectilePrefab, spawnTransform);
        projectile.SetVelocity(direction * projectileSpeed);
    }
}
