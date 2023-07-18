using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Unity.VisualScripting;

namespace Enemies.Salat
{
    public class Salat : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float shootRate, projectileActivationDelay, projectileSpeed, moveSpeed;
        [SerializeField] private Transform shootStartTransform;
        [SerializeField] Projectile[] projectiles;

        private float _lastShootTime = 0;
        private int _lastProjectileIndex = 0;
        private State _state;
        private Transform _pointToStartAttack;

        private enum State
        {
            GoingToPoint,
            Refilling,
            WaitingForRefill,
            Attacking
        }

        private void Start()
        {
            _pointToStartAttack = GameObject.Find("PointToStartAttack").transform;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.GoingToPoint:
                    float xDistance = transform.position.x - _pointToStartAttack.position.x;
                    if (Mathf.Abs(xDistance) < 0.1f)
                    {
                        _state = State.Refilling;
                        rb.velocity = Vector2.zero;
                    }
                    else
                        rb.velocity = Vector2.left * moveSpeed;
                    break;

                case State.Refilling:
                    StartCoroutine(RefillingCoroutine());
                    break;

                case State.WaitingForRefill:
                    break;

                case State.Attacking:
                    Shoot();
                    break;

                default:
                    break;
            }
        }

        private IEnumerator RefillingCoroutine()
        {
            _state = State.WaitingForRefill;
            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(projectileActivationDelay);
            }
            _state = State.Attacking;
        }

        private void Shoot()
        {
            if (Time.time < _lastShootTime + shootRate)
                return;

            Projectile projectilePrefab = projectiles[_lastProjectileIndex];
            Projectile projectile = Instantiate(projectilePrefab, _pointToStartAttack);
            projectilePrefab.gameObject.SetActive(false);

            projectile.SetVelocity(Vector2.left * projectileSpeed);

            _lastProjectileIndex++;
            _lastShootTime = Time.time;

            if (_lastProjectileIndex == projectiles.Length)
            {
                _state = State.Refilling;
                _lastProjectileIndex = 0;
            }
        }
    }
}
