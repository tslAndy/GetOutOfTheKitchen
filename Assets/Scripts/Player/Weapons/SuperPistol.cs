using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPistol : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate, projectileSpeed;

    private float _lastShotTime;


    public override void Attack(Vector2 direction)
    {

        if (Time.time < _lastShotTime + fireRate)
            return;

        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.SetVelocity(direction * projectileSpeed);
    }
     public override void RotateWeapon(Vector2 direction)
    {
        transform.up = direction;
    }
}
