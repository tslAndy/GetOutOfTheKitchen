using UnityEngine;

public class PlayerSingleton : Singleton<PlayerSingleton>
{
    [SerializeField] private Transform player;
    public Transform Player => player;
}
