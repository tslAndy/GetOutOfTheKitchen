using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemies.Salat
{
    public class Salat : MonoBehaviour
    {
        [SerializeField] private float shootRate, projectileSpeed;
        [SerializeField] private Transform shootStartTransform;
        [SerializeField] Projectile[] projectiles;

        private float _lastShootTime = 0;
        private int _lastProjectileIndex = 0;

        private void Update()
        {
            if (Time.time >= _lastShootTime + shootRate && _lastProjectileIndex < projectiles.Length)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Projectile projectile = projectiles[_lastProjectileIndex];
            projectile.transform.position = shootStartTransform.position;
            Vector2 direction = (PlayerSingleton.Instance.transform.position - projectile.transform.position).normalized;

            Debug.Log(direction);
            projectile.SetVelocity(direction * projectileSpeed);

            _lastProjectileIndex++;
            _lastShootTime = Time.time;
        }
    }
}
