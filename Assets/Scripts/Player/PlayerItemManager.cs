using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] PlayerScripts player;

    public void EquipItem(Item item)
    {
        switch (item.GetItemType()) 
        {
            case ItemType.Weapon:
                player.ChangeWeapon((Weapon)item);
                break;

            default:
                break;
        }
    }
}
