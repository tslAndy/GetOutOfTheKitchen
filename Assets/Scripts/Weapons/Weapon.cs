using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : Item
    {
        public override ItemType GetItemType() => ItemType.Weapon;
        public abstract void OnMainShootStarted(Vector2 direction);
        public abstract void OnMainShootPerformed(Vector2 direction);
        public abstract void OnMainShootCanceled(Vector2 direction);

        public abstract void OnAdditionalShootStarted(Vector2 direction);
        public abstract void OnAdditionalShootPerformed(Vector2 direction);
        public abstract void OnAdditionalShootCanceled(Vector2 direction);
    }
}
