using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text caloriesText;
    [SerializeField] private PlayerItemManager playerItemManager;

    private void Update()
    {
        int calories = CaloriesCounterSingleton.Instance.Calories;
        caloriesText.SetText($"Calories: {calories}");
    }

    public void BuyItem(GoodScriptable goodScriptable)
    {
        if (CaloriesCounterSingleton.Instance.Calories < goodScriptable.ItemPrice)
            return;

        CaloriesCounterSingleton.Instance.TakeCalories(goodScriptable.ItemPrice);
        playerItemManager.EquipItem(goodScriptable.Item);
    }
}
