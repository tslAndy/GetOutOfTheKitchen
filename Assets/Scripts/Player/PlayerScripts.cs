using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerScripts : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private Weapon weapon;
        
        public PlayerMovement Movement => movement;
        public Weapon CurrentWeapon => weapon;

        public void ChangeWeapon(Weapon newWeapon) => weapon = newWeapon;
    }
}

