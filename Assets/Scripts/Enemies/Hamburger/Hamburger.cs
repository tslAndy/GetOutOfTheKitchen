using UnityEngine;
using Player;

namespace Enemies.Hamburger
{
    public class Hamburger : MonoBehaviour
    {
        [SerializeField] private float jumpSpeed, runSpeed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D coll;

        private State _state;
        private Collider2D _shelfColl;
        private bool _collidedWithFloor;
        private float direction;

        private enum State
        {
            Falling,
            Jumping,
            Air,
            Running
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Falling:
                    if (_shelfColl != null)
                    {
                        _state = State.Jumping;
                    }
                    break;

                case State.Jumping:
                    rb.velocity = Vector2.up * jumpSpeed;
                    Physics2D.IgnoreCollision(coll, _shelfColl);
                    _state = State.Air;
                    break;

                case State.Air:
                    if (_collidedWithFloor)
                    {
                        float playerX = PlayerSingleton.Instance.Player.position.x;
                        direction = Mathf.Sign(playerX - transform.position.x);
                        _state = State.Running;
                    }
                    break;

                case State.Running:
                    rb.velocity = Vector2.right * direction * runSpeed;
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
                _collidedWithFloor = true;
            else if (collision.gameObject.CompareTag("Shelf"))
                _shelfColl = collision.collider;
            else if (collision.gameObject.CompareTag("Wall"))
                Destroy(gameObject);
        }
    }
}