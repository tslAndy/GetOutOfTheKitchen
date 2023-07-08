using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Player;

namespace Enemies.Icecream
{
    public class Icecream : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int projectilesAmount, attacksAmount;
        [SerializeField] private float goingAwaySpeed, spawnRate, projectileSpeed, angleBetweenProjectiles;

        private float _lastSpawnTime;
        private int _attacksDone;
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
                    if (_attacksDone == attacksAmount)
                        _state = State.GoingAway;
                    else if (Time.time >= _lastSpawnTime + spawnRate)
                        Shoot();
                    break;

                case State.GoingAway:
                    rb.velocity = Vector2.right * goingAwaySpeed;
                    break;
            }
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
            _attacksDone++;
            _lastSpawnTime = Time.time;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
                Destroy(gameObject);
        }
    }
}