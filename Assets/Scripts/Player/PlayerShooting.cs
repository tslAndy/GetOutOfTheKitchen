using System;
using UnityEngine;


namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform firePoint;                               // point from where projectiles gonna spawn 
        [SerializeField] private float fireRate, projectileSpeed;

        private float _lastShotTime;

        public void Shoot(Vector2 direction)
        {
            if (Time.time < _lastShotTime + fireRate)
                return;

            Projectile projectile = Instantiate(projectilePrefab, firePoint);
            projectile.SetVelocity(direction * projectileSpeed);
        }
        public void RotateWeapon(Vector2 direction)
        {
            transform.up = direction;
        }
    }
}
