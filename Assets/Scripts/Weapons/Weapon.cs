using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : Item
    {
        public override ItemType GetItemType() => ItemType.Weapon;
        public abstract void OnShootStarted(Vector2 direction);
        public abstract void OnShootPerformed(Vector2 direction);
        public abstract void OnShootCanceled(Vector2 direction);
    }
}
