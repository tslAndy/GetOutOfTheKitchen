using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Enemies.Salat
{
    public class Salat : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float shootRate, projectileSpeed, goingAwaySpeed;
        [SerializeField] private Transform shootStartTransform;
        [SerializeField] Projectile[] projectiles;

        private float _lastShootTime = 0;
        private int _lastProjectileIndex = 0;
        private State _state;

        private enum State
        {
            Shooting,
            GoingAway
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Shooting:
                    if (_lastProjectileIndex >= projectiles.Length)
                        _state = State.GoingAway;
                    else if (Time.time >= _lastShootTime + shootRate)
                        Shoot();
                    break;

                case State.GoingAway:
                    rb.velocity = Vector2.right * goingAwaySpeed;
                    break;
            }
        }

        private void Shoot()
        {
            Projectile projectile = projectiles[_lastProjectileIndex];
            projectile.transform.position = shootStartTransform.position;
            Vector2 direction = (PlayerSingleton.Instance.transform.position - projectile.transform.position).normalized;

            projectile.SetVelocity(direction * projectileSpeed);

            _lastProjectileIndex++;
            _lastShootTime = Time.time;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
                Destroy(gameObject);
        }
    }
}
