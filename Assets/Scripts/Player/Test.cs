using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayerItemManager playerItemManager;
    [SerializeField] private Weapon weaponToEquip;

    private void Start()
    {
        playerItemManager.EquipItem(weaponToEquip);
    }
}
