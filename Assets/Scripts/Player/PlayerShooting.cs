using UnityEngine;
using Enemies.Icecream;
using Pooling;
using Input;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private IcecreamBall icecreamBallPrefab;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float fireRate;

    private float _lastShotTime;

    private PoolMemory<IcecreamBall> _icecreamBallPoolMemory;

    private void Start()
    {
        _icecreamBallPoolMemory = PoolsManager.Instance.GetPoolMemory<IcecreamBall>();
    }

    private void Update()
    {
        if (Time.time < _lastShotTime + fireRate || !inputHandler.Shooting)
            return;

        Vector2 direction = (inputHandler.MouseVector - transform.position).normalized;
        IcecreamBall icecreamBall = _icecreamBallPoolMemory.GetPoolObject(icecreamBallPrefab);
        icecreamBall.transform.position = transform.position;
        icecreamBall.ChangeDirection(direction);
        _lastShotTime = Time.time;
    }
}
