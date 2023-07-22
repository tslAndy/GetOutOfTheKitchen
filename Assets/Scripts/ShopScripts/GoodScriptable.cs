using UnityEngine;
using Weapons;

namespace ShopScripts
{
    [CreateAssetMenu(menuName = "ShopSystem/Good")]
    public class GoodScriptable : ScriptableObject
    {
        [SerializeField] private Item item;
        [SerializeField] private string goodName;
        [SerializeField] private int goodPrice;
        [SerializeField] private Texture goodTexture;

        public Item Item => item;
        public string GoodName => goodName;
        public int GoodPrice => goodPrice;
        public Texture GoodTexture => goodTexture;
    }
}
