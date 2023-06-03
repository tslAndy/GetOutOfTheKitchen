using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Extensions;


namespace Enemies.Icecream
{
    public class Icecream : PoolObject<Icecream>
    {
        [SerializeField] private float speed, attackDistance;
        [SerializeField] private int angleBetweenBalls, attacksAmount, timeBetweenAttacks;
        [SerializeField] private IcecreamBall[] icecreamBalls;

        private bool _destroyAfterAttack;

        private enum IcecreamState
        {
            Idle,
            Fly,
            Attack,
            WaitForAttackEnd
        }
        private IcecreamState _state = IcecreamState.Idle;
        private PoolMemory<IcecreamBall> _icecreamBallPoolMemory;

        private void Start()
        {
            _icecreamBallPoolMemory = PoolsManager.Instance.GetPoolMemory<IcecreamBall>();
        }

        private void Update()
        {
            switch (_state)
            {
                case IcecreamState.Idle:
                    if (PlayerSingleton.Instance.Player != null)
                        _state = IcecreamState.Fly;
                    break;

                case IcecreamState.Fly:
                    Vector3 posDiff = (PlayerSingleton.Instance.Player.position - transform.position);
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, posDiff);
                    if (posDiff.magnitude <= attackDistance)
                    {
                        _state = IcecreamState.Attack;
                    }

                    else
                        transform.position += posDiff.normalized * speed * Time.deltaTime;
                    break;

                case IcecreamState.Attack:
                    Attack();
                    _state = IcecreamState.WaitForAttackEnd;
                    break;

                case IcecreamState.WaitForAttackEnd:
                    break;

                default:
                    break;
            }

        }

        private void Attack() => StartCoroutine(AttackCoroutine());

        private IEnumerator AttackCoroutine()
        {
            Transform playerTransform = PlayerSingleton.Instance.Player;

            float centerIndex = icecreamBalls.Length / 2.0f;

            for (int i = 0; i < icecreamBalls.Length; i++)
            {
                icecreamBalls[i].gameObject.SetActive(false);
            }

            for (int j = 0; j < attacksAmount; j++)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                for (int i = 0; i < icecreamBalls.Length; i++)
                {
                    IcecreamBall icecreamBall = _icecreamBallPoolMemory.GetPoolObject(icecreamBalls[i]);
                    icecreamBall.transform.position = icecreamBalls[i].transform.position;

                    float angleToRotate = (i - centerIndex) * angleBetweenBalls;
                    icecreamBall.ChangeDirection(direction.Rotate(angleToRotate));
                }

                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            for (int i = 0; i < icecreamBalls.Length; i++)
            {
                icecreamBalls[i].gameObject.SetActive(true);
            }

            if (_destroyAfterAttack)
                Destroy(gameObject);
            else
                DestroyAction(this);
        }


        public override void BeforeReturnToPool()
        {
            transform.rotation = Quaternion.identity;
            _state = IcecreamState.Idle;
        }

        public override void OnPoolDestroy()
        {
            _destroyAfterAttack = true;
        }
    }
}