using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PlayerScripts
{
    public class PlayerItemManager : MonoBehaviour
    {
        [SerializeField] Player player;

        public void EquipItem(Item item)
        {
            switch (item.GetItemType())
            {
                case ItemType.Weapon:
                    player.WeaponManager.ChangeWeapon((Weapon)item);
                    break;

                default:
                    break;
            }
        }
    }
}
