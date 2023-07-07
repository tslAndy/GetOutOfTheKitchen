using System;
using UnityEngine;


namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float fireRate, projectileSpeed;

        private float _lastShotTime;

        public void Shoot(Vector2 direction)
        {
            if (Time.time < _lastShotTime + fireRate)
                return;

            Projectile projectile = Instantiate(projectilePrefab, transform);
            projectile.SetVelocity(direction * projectileSpeed);
        }
    }
}
