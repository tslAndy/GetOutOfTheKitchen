using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using PlayerScripts;

namespace PlayerScripts.PlayerWeapons
{
    public class Pistol : Weapon
    {
        [SerializeField] private Projectile mainProjectilePrefab, additionalProjectilePrefab;
        [SerializeField] private Transform projectileSpawnTransform;
        [SerializeField] private float mainFireRate, additionalFireRate, speed;

        private bool _mainShootPressed, _additionalShootPressed;
        private float _lastMainShotTime, _lastAdditionalShotTime;

        private void Update()
        {
            if (!(_mainShootPressed || _additionalShootPressed))
                return;

            if (_mainShootPressed && Time.time > _lastMainShotTime + mainFireRate)
            {
                SpawnProjectile(mainProjectilePrefab);
                _lastMainShotTime = Time.time;
            }

            if (_additionalShootPressed && Time.time > _lastAdditionalShotTime + additionalFireRate)
            {
                SpawnProjectile(additionalProjectilePrefab);
                _lastAdditionalShotTime = Time.time;
            }
        }

        private void SpawnProjectile(Projectile projectileToSpawn)
        {
            Vector2 shootDirection = InputHandler.Instance.GetMouseDirection(projectileSpawnTransform);
            Projectile projectile = Instantiate(projectileToSpawn, projectileSpawnTransform);
            projectile.transform.SetParent(null);
            projectile.SetVelocity(shootDirection * speed);
        }

        public override void OnMainShootStarted() => _mainShootPressed = true;
        public override void OnMainShootCanceled() => _mainShootPressed = false;

        public override void OnAdditionalShootStarted() => _additionalShootPressed = true;
        public override void OnAdditionalShootCanceled() => _additionalShootPressed = false;
    }
}
