using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using PlayerScripts;

namespace PlayerScripts.PlayerWeapons
{
    public class Pistol : Weapon
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private Transform projectileSpawnTransform;
        [SerializeField] private float fireRate, speed;

        private bool _shootPressed;
        private Vector2 _shootDirection;
        private float _lastShotTime;

        private void Update()
        {
            if (!_shootPressed)
                return;

            if (Time.time < _lastShotTime + fireRate)
                return;

            _shootDirection = InputHandler.Instance.GetMouseDirection(projectileSpawnTransform);
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform);
            projectile.transform.SetParent(null);
            projectile.SetVelocity(_shootDirection * speed);

            _lastShotTime = Time.time;
        }

        public override void OnShootStarted(Vector2 direction) => _shootPressed = true;
        public override void OnShootPerformed(Vector2 direction) { }
        public override void OnShootCanceled(Vector2 direction) => _shootPressed = false;
    }
}
