using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoodUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private GoodScriptable goodScriptableFromInspector;
    [SerializeField] private TMP_Text itemName, itemPrice;
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Button buyButton;

    private void Start()
    {
        if (goodScriptableFromInspector != null)
            InitGood(goodScriptableFromInspector);
    }

    public void InitGood(GoodScriptable goodScriptable)
    {
        itemName.SetText(goodScriptable.ItemName);
        itemPrice.SetText($"Buy ({goodScriptable.ItemPrice})");
        itemImage.texture = goodScriptable.ItemTexture;
        buyButton.onClick.AddListener(() => shop.BuyItem(goodScriptable));
    }
}
