using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed, jumpSpeed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D coll;

        private bool _grounded, _canMakeDoubleJump;
        private Collider2D _platformCollider;

        // Moving on the ground
        public void Move(Vector2 direction)
        {
            if (!_grounded)
            {
                if (direction.y < 0 && rb.velocity.y > -jumpSpeed)
                    rb.velocity = new Vector2(rb.velocity.x, -jumpSpeed);
                return;
            }

            // if standing on platform and want to jump off
            if (_platformCollider != null && direction.y < 0)
                StartCoroutine((JumpOffPlatformCoroutine()));
            else
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }

        public void Jump()
        {
            if (_grounded)
            {
                transform.position += Vector3.up * 0.01f;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                _grounded = false;
            }
            else if (_canMakeDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                _canMakeDoubleJump = false;
            }
        }

        private IEnumerator JumpOffPlatformCoroutine()
        {
            _grounded = false;
            Physics2D.IgnoreCollision(coll, _platformCollider);
            yield return new WaitForSeconds(0.4f);
            Physics2D.IgnoreCollision(coll, _platformCollider, false);
            _platformCollider = null;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("OneWayPlatform"))
            {
                _grounded = true;
                _canMakeDoubleJump = true;
            }

            if (col.gameObject.CompareTag("OneWayPlatform"))
                _platformCollider = col.collider;
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("OneWayPlatform"))
                _grounded = false;
        }
    }
}

