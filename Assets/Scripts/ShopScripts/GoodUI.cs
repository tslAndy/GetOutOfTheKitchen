using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ShopScripts
{
    public class GoodUI : MonoBehaviour
    {
        [SerializeField] private Shop shop;
        [SerializeField] private GoodScriptable goodScriptable;
        [SerializeField] private TMP_Text goodName, goodPrice;
        [SerializeField] private RawImage goodImage;
        [SerializeField] private Button buyButton;

        private void Awake()
        {
            goodName.SetText(goodScriptable.GoodName);
            goodPrice.SetText($"Buy ({goodScriptable.GoodPrice})");
            goodImage.texture = goodScriptable.GoodTexture;
            buyButton.onClick.AddListener(() => shop.BuyItem(goodScriptable));
        }
    }
}

