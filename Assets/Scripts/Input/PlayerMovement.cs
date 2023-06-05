using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float moveSpeed;

        private bool _grounded;

        private enum PlayerState
        {
            Idle,
            Moving,
        }

        private PlayerState _state = PlayerState.Idle;

        private void Update()
        {
            switch (_state)
            {
                case PlayerState.Idle:
                    if (inputHandler.MoveVector != Vector2.zero)
                    {
                        _state = PlayerState.Moving;
                    }
                    break;

                case PlayerState.Moving:
                    if (!_grounded)
                        return;

                    if (inputHandler.MoveVector != Vector2.zero)
                    {
                        rb.velocity = new Vector2(inputHandler.MoveVector.x * moveSpeed, rb.velocity.y);
                    }
                    else if (inputHandler.MoveVector == Vector2.zero)
                    {
                        _state = PlayerState.Idle;
                        rb.velocity = Vector2.zero;
                    }

                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Floor"))
                _grounded = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Floor"))
                _grounded = false;
        }
    }
}

