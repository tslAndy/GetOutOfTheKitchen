using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Extensions;
using Player;


namespace Enemies.Icecream
{
    public class Icecream : PoolObject<Icecream>
    {
        [SerializeField] private float speed, attackDistance;
        [SerializeField] protected int angleBetweenBalls, attacksAmount, timeBetweenAttacks;
        [SerializeField] private IcecreamBall[] icecreamBalls;
        [SerializeField] private float rotateSpeed;

        protected bool _destroyAfterAttack;

        private enum IcecreamState
        {
            Idle,
            Fly,
            Attack,
            WaitForAttackEnd
        }
        private IcecreamState _state = IcecreamState.Idle;
        private PoolMemory<IcecreamBall> _icecreamBallPoolMemory;
        protected IcecreamBall[] tempIcecreamBalls;

        private void Start()
        {
            _icecreamBallPoolMemory = PoolsManager.Instance.GetPoolMemory<IcecreamBall>();
            tempIcecreamBalls = new IcecreamBall[icecreamBalls.Length];
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
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, posDiff);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
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

        protected virtual IEnumerator AttackCoroutine()
        {
            Transform playerTransform = PlayerSingleton.Instance.Player;

            float centerIndex = icecreamBalls.Length / 2.0f;

            ChangeIcecreamBallsState(false);

            for (int j = 0; j < attacksAmount; j++)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                InitTempBalls();
                for (int i = 0; i < tempIcecreamBalls.Length; i++)
                {
                    IcecreamBall icecreamBall = tempIcecreamBalls[i];

                    float angleToRotate = (i - centerIndex) * angleBetweenBalls;
                    icecreamBall.ChangeDirection(direction.Rotate(angleToRotate));
                }

                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            ChangeIcecreamBallsState(true);
            CheckDeath();
        }

        protected void ChangeIcecreamBallsState(bool active)
        {
            for (int i = 0; i < icecreamBalls.Length; i++)
                icecreamBalls[i].gameObject.SetActive(active);
        }

        protected void InitTempBalls()
        {
            for (int i = 0; i < icecreamBalls.Length; i++)
            {
                tempIcecreamBalls[i] = _icecreamBallPoolMemory.GetPoolObject(icecreamBalls[i]);
                tempIcecreamBalls[i].gameObject.transform.position = icecreamBalls[i].gameObject.transform.position;
            }
        }

        protected void CheckDeath()
        {
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