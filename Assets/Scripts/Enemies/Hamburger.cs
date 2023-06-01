using System;
using Pooling;
using UnityEngine;

namespace Enemies
{
    public class Hamburger : PoolObject<Hamburger>
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D coll;

        private HamburgerState _state;
        private bool _onShelf, _onFloor, _collidedWithPlayer;
        private Collider2D _shelfCollider;
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = PlayerSingleton.Instance.Player.transform;
        }

        private void Update()
        {
            if (_playerTransform == null)
                return;

            switch (_state)
            {
                case HamburgerState.Idle:
                    if (_onShelf)
                    {
                        rb.velocity = HamburgerData.JumpVector;
                        Physics2D.IgnoreCollision(coll, _shelfCollider);
                        _state = HamburgerState.Jumping;
                    }
                    break;
                case HamburgerState.Jumping:
                    if (_onFloor)
                        _state = HamburgerState.Running;
                    break;
                case HamburgerState.Running:
                    float direction = Mathf.Sign(_playerTransform.position.x - transform.position.x);
                    rb.velocity = direction > 0 ? HamburgerData.RunRightVector : HamburgerData.RunLeftVector;

                    if (_collidedWithPlayer)
                    {
                        rb.velocity = Vector2.zero;
                        Physics2D.IgnoreCollision(coll, _shelfCollider, false);
                        _onShelf = _onFloor = _collidedWithPlayer = false;
                        _state = HamburgerState.Idle;
                        DestroyAction(this);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Floor"))
                _onFloor = true;
            else if (col.gameObject.CompareTag("Shelf"))
            {
                _onShelf = true;
                _shelfCollider = col.collider;
            }
            else if (col.gameObject.CompareTag("Player"))
                _collidedWithPlayer = true;
        }
    }

    public static class HamburgerData
    {
        public static readonly float RunSpeed = 5f, JumpSpeed = 8f;
        public static readonly Vector2 JumpVector = Vector2.up * JumpSpeed;
        public static readonly Vector2 RunRightVector = Vector2.right * RunSpeed;
        public static readonly Vector2 RunLeftVector = Vector2.left * RunSpeed;
    }

    enum HamburgerState
    {
        Idle,
        Jumping,
        Running
    }
}