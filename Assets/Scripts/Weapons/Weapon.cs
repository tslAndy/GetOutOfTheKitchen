using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public override ItemType GetItemType() => ItemType.Weapon;
    public abstract void Attack(Vector2 direction);

    public abstract void RotateWeapon(Vector2 direction);
}
