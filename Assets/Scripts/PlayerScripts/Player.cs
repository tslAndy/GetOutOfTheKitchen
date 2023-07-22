using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerWeaponManager weaponManager;
        [SerializeField] private PlayerItemManager itemManagerManager;

        public PlayerMovement Movement => movement;
        public PlayerWeaponManager WeaponManager => weaponManager;
        public PlayerItemManager ItemManager => itemManagerManager;
    }
}

