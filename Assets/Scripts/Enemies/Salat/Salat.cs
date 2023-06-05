using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using Enemies.Icecream;

namespace Salat
{
    public class Salat : Icecream
    {
        [SerializeField] private Transform attackFromTransform;

        protected override IEnumerator AttackCoroutine()
        {
            Transform playerTransform = PlayerSingleton.Instance.Player;
            ChangeIcecreamBallsState(false);
            InitTempBalls();

            for (int i = 0; i < tempIcecreamBalls.Length; i++)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                IcecreamBall icecreamBall = tempIcecreamBalls[i];
                icecreamBall.transform.position = attackFromTransform.position;
                icecreamBall.ChangeDirection(direction);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            ChangeIcecreamBallsState(true);
            CheckDeath();

        }
    }
}

