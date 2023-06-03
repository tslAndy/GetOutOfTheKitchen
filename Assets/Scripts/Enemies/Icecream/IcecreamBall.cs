using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

namespace Enemies.Icecream
{
    public class IcecreamBall : PoolObject<IcecreamBall>
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;

        private bool _destroyOnCollision;

        public void ChangeDirection(Vector2 direction) => rb.velocity = direction * speed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_destroyOnCollision)
                Destroy(gameObject);
            else
                DestroyAction(this);
        }

        public override void BeforeReturnToPool()
        {
            rb.velocity = Vector2.zero;
        }

        public override void OnPoolDestroy()
        {
            _destroyOnCollision = true;
        }
    }
}
