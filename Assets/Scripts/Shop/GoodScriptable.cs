using UnityEngine;

[CreateAssetMenu(menuName = "Good")]
public class GoodScriptable : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemPrice;
    [SerializeField] private Texture itemTexture;
    [SerializeField] private Item item;

    public string ItemName => itemName;
    public int ItemPrice => itemPrice;
    public Texture ItemTexture => itemTexture;
    public Item Item => item;
}
