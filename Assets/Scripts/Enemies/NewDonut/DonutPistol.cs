using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class DonutPistol : Weapon
{
    [SerializeField] private Projectile mainProjectilePrefab;
    [SerializeField] private float mainFireRate, speed;
    [SerializeField] private List<Transform> pointsToShootFrom;

    private bool _mainShootPressed;
    private float _lastMainShotTime;

    private void Update()
    {
        if (!_mainShootPressed)
            return;

        if (Time.time > _lastMainShotTime + mainFireRate)
        {
            SpawnProjectile(mainProjectilePrefab);
            _lastMainShotTime = Time.time;
        }
    }

    private void SpawnProjectile(Projectile projectileToSpawn)
    {
        foreach (Transform trans in pointsToShootFrom)
        {
            Projectile projectile = Instantiate(projectileToSpawn, trans);
            projectile.transform.SetParent(null);
            projectile.SetVelocity(Vector2.left * speed);    
        }

    }

    public override void OnMainShootStarted() => _mainShootPressed = true;
    public override void OnMainShootCanceled() => _mainShootPressed = false;

}
