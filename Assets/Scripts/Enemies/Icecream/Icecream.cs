using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Player;

namespace Enemies.Icecream
{
    public class Icecream : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int projectilesAmount;
        [SerializeField] private float spawnRate, projectileSpeed, angleBetweenProjectiles;

        private float _lastSpawnTime;

        private void Update()
        {
            if (Time.time >= _lastSpawnTime + spawnRate)
                Shoot();
        }

        private void Shoot()
        {
            int centerIndex = projectilesAmount / 2;
            float direction = Mathf.Sign(PlayerSingleton.Instance.Player.position.x - transform.position.x);

            for (int i = 0; i < projectilesAmount; i++)
            {
                Vector2 velocity = (direction > 0) ? Vector2.right : Vector2.left;
                velocity = velocity.Rotate((centerIndex - i) * angleBetweenProjectiles) * projectileSpeed;
                Projectile projectile = Instantiate(projectilePrefab, transform);
                projectile.SetVelocity(velocity);
            }
            _lastSpawnTime = Time.time;
        }
    }
}