using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract ItemType GetItemType();
}

public enum ItemType
{
    Weapon,
    Armor
}
