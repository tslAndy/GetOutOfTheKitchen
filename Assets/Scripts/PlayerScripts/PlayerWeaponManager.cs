using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PlayerScripts
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        [SerializeField] private Transform weaponTransform;
        [SerializeField] private Weapon weaponPrefab;

        private Weapon _weapon;
        public Weapon CurrentWeapon
        {
            get => _weapon;
            private set => _weapon = value;
        }

        private void Awake()
        {
            ChangeWeapon(weaponPrefab);
        }

        public void ChangeWeapon(Weapon newWeaponPrefab)
        {
            if (CurrentWeapon != null)
                Destroy(CurrentWeapon.gameObject);

            CurrentWeapon = Instantiate(newWeaponPrefab, weaponTransform);
        }
    }
}
