using System;
using UnityEngine;


namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Weapon currentWeapon;
        public Weapon CurrentWeapon => currentWeapon;
    }
}
