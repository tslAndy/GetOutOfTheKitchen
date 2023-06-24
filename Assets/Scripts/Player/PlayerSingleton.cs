using UnityEngine;

namespace Player
{
    public class PlayerSingleton : Singleton<PlayerSingleton>
    {
        [SerializeField] private Transform player;
        public Transform Player => player;
    }
}
