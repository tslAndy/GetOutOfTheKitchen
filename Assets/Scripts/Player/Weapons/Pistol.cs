using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Pistol")]
public class Pistol : Weapon
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate, projectileSpeed;

    private float _lastShotTime;

    private void OnEnable()
    {
        _lastShotTime = 0f;
    }

    public override void Attack(Vector2 direction, Transform spawnTransform)
    {
        if (Time.time < _lastShotTime + fireRate)
            return;

        Projectile projectile = Instantiate(projectilePrefab, spawnTransform);
        projectile.SetVelocity(direction * projectileSpeed);
        _lastShotTime = Time.time;
    }

}
