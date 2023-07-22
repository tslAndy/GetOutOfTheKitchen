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
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private float speed, spawnRate, projectileSpeed, angleBetweenProjectiles;
        [SerializeField] private Projectile[] projectiles;

        private float _lastSpawnTime;
        private int _attacksDone;
        private Transform _pointToStartAttack;
        private State _state;

        private enum State
        {
            FlyingToPoint,
            Shooting
        }

        private void Start()
        {
            _pointToStartAttack = GameObject.Find("PointToStartAttack").transform;
        }
        private void Update()
        {
            switch (_state)
            {
                case State.FlyingToPoint:
                    float posDiff = _pointToStartAttack.position.x - transform.position.x;
                    if (Mathf.Abs(posDiff) <= 0.1f)
                    {
                        rb.velocity = Vector2.zero;
                        _state = State.Shooting;
                        break;
                    }

                    rb.velocity = Vector2.right * speed * Mathf.Sign(posDiff);
                    break;

                case State.Shooting:
                    if (Time.time >= _lastSpawnTime + spawnRate)
                        Shoot();
                    break;
            }
        }

        private void Shoot()
        {
            int projectilesAmount = projectiles.Length;
            int centerIndex = projectilesAmount / 2;
            float direction = Mathf.Sign(PlayerSingleton.Instance.Player.position.x - transform.position.x);

            for (int i = 0; i < projectilesAmount; i++)
            {
                Vector2 velocity = (direction > 0) ? Vector2.right : Vector2.left;
                velocity = velocity.Rotate((centerIndex - i) * angleBetweenProjectiles) * projectileSpeed;
                Projectile projectile = Instantiate(projectiles[i], spawnTransform);
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