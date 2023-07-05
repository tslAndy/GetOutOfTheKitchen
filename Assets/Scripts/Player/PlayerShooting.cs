using System;
using UnityEngine;
using Enemies.Icecream;
using Pooling;


namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private IcecreamBall bulletPrefab;
        [SerializeField] private float fireRate;
        
        private PoolMemory<IcecreamBall> _bulletPoolMemory;
        private float _lastShotTime;

        private void Start()
        {
            _bulletPoolMemory = PoolsManager.Instance.GetPoolMemory<IcecreamBall>();
        }

        public void Shoot(Vector2 direction)
        {
            if(GameManager.instance.gameState == GameManager.GameState.Continuing)
            {
                float shotTime = _lastShotTime + fireRate;
                if (Time.time < shotTime)
                    return;

                _lastShotTime = shotTime;

                IcecreamBall bullet = _bulletPoolMemory.GetPoolObject(bulletPrefab);
                bullet.transform.position = transform.position;
                bullet.ChangeDirection(direction);
            } 
        }
    }
}
