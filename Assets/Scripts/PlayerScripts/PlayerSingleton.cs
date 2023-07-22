using UnityEngine;

namespace PlayerScripts
{
    public class PlayerSingleton : Singleton<PlayerSingleton>
    {
        [SerializeField] private Transform player;
        public Transform Player => player;
    }
}
