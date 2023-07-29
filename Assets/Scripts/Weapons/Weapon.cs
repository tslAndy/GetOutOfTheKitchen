using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : Item
    {
        public override ItemType GetItemType() => ItemType.Weapon;
        public virtual void OnMainShootStarted() { }
        public virtual void OnMainShootCanceled() { }

        public virtual void OnAdditionalShootStarted() { }
        public virtual void OnAdditionalShootCanceled() { }
    }
}
