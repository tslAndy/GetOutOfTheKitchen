using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Item : MonoBehaviour
    {
        public abstract ItemType GetItemType();
    }

    public enum ItemType
    {
        Armor,
        Weapon
    }
}
