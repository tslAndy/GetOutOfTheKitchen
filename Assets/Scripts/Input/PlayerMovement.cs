using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private float moveSpeed, jumpSpeed;
        [SerializeField] private PlayerOneWayPlatform playerOneWayPlatform;            // �����
        private Collider2D playerCollider;                                      //   ����������

        private bool _grounded, _canMakeDoubleJump;

        private enum PlayerState
        {
            Idle,
            Moving,
            Jumping
        }

        private PlayerState _state = PlayerState.Idle;

        private Vector2 _idleVector = Vector2.zero;

        private void Awake()                                    // �������� ����� Awake
        {
            playerCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            switch (_state)
            {
                case PlayerState.Idle:
                    if (inputHandler.Jumping)
                    {
                        Jump();
                        _state = PlayerState.Jumping;
                    } 
                    else if (inputHandler.MoveVector != _idleVector)
                    {
                        GoThroughtPlatform(playerCollider);                      // ����������� �����  GoThroughtPlatform � idle case
                        _state = PlayerState.Moving;
                        
                    }
                  

                    break;

                case PlayerState.Moving:
                    if (!_grounded)
                        return;

                    if (inputHandler.Jumping)
                    {
                        Jump();
                        _state = PlayerState.Jumping;
                    }

                    else if (inputHandler.MoveVector != _idleVector)
                    {
                        if(inputHandler.MoveVector.y <= -1f)
                        {
                            GoThroughtPlatform(playerCollider);                              // ����������� �����  GoThroughtPlatform � moving case
                        }
                       
                        rb.velocity = new Vector2(inputHandler.MoveVector.x * moveSpeed, rb.velocity.y);
                       
                    }

                    else if (inputHandler.MoveVector == _idleVector)
                    {
                        _state = PlayerState.Idle;
                        rb.velocity = _idleVector;
                    }

                    break;

                case PlayerState.Jumping:
                    if (inputHandler.Jumping && _canMakeDoubleJump)
                    {
                        Jump();
                        _canMakeDoubleJump = false;
                    }
                    else if (_grounded)
                    {
                        _state = inputHandler.MoveVector == _idleVector ? PlayerState.Idle : PlayerState.Moving;
                    }
                    break;
            }
        }

        private void Jump()
        {
            inputHandler.EndJump();
            transform.position += Vector3.up * 0.01f;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        private void GoThroughtPlatform(Collider2D playerCollider)                                      // �����, ��� �������� ����������� �������� (������� ������) ����� � ���������
        {
            if (inputHandler.MoveVector.y <= -1f && playerOneWayPlatform.OneWayPlatform != null)
            {
                StartCoroutine(playerOneWayPlatform.DisableCollision(playerCollider));                                                           
                Debug.Log("Down From Platform");
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("OneWayPlatform"))
            {
                _grounded = true;
                _canMakeDoubleJump = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("OneWayPlatform"))
                _grounded = false;
        }
    }
}

