using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Enemies.Icecream;


namespace Nuggets
{
    public class Nugget : PoolObject<Nugget>
    {
        [SerializeField] private int amountOfNuggetsInAttack;
        [SerializeField] private float spawnRate;
        [SerializeField] private IcecreamBall nuggetsPrefab;

        private PoolMemory<IcecreamBall> _nuggets;
        private IcecreamBall[] _tempNuggets;

        private Vector2 _nuggetStartVector = Vector2.up;

        private void Start()
        {
            _nuggets = PoolsManager.Instance.GetPoolMemory<IcecreamBall>();
            _tempNuggets = new IcecreamBall[amountOfNuggetsInAttack];
        }

        private enum NuggetState
        {
            Idle,
            Spawning,
            WaitingForSpawnEnd,
            Attacking,
            WaitingForAttackEnd
        }

        private NuggetState _state;

        private void Update()
        {
            switch (_state)
            {
                case NuggetState.Idle:
                    if (PlayerSingleton.Instance.Player != null)
                        _state = NuggetState.Spawning;
                    break;

                case NuggetState.Spawning:
                    StartCoroutine(SpawnCoroutine());
                    _state = NuggetState.WaitingForSpawnEnd;
                    break;

                case NuggetState.WaitingForSpawnEnd:
                    break;

                case NuggetState.Attacking:
                    StartCoroutine(AttackCoroutine());
                    _state = NuggetState.WaitingForAttackEnd;
                    break;

                case NuggetState.WaitingForAttackEnd:
                    break;
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            for (int i = 0; i < amountOfNuggetsInAttack; i++)
            {
                IcecreamBall nugget = _nuggets.GetPoolObject(nuggetsPrefab);
                nugget.transform.position = transform.position;
                nugget.ChangeDirection(_nuggetStartVector);
                _tempNuggets[i] = nugget;
                yield return new WaitForSeconds(spawnRate);
            }
            yield return new WaitForSeconds(0.05f);
            _state = NuggetState.Attacking;
        }

        private IEnumerator AttackCoroutine()
        {
            for (int i = 0; i < amountOfNuggetsInAttack; i++)
            {
                IcecreamBall nugget = _tempNuggets[i];
                Vector2 direction = (PlayerSingleton.Instance.Player.position - nugget.transform.position).normalized;
                nugget.ChangeDirection(direction);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3);
            _state = NuggetState.Idle;
        }

        public override void BeforeReturnToPool()
        {
            _state = NuggetState.Idle;
        }

        public override void OnPoolDestroy() { }
    }
}