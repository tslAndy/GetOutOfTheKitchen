using UnityEngine;
using TMPro;
using PlayerScripts;
using Other;

namespace ShopScripts
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private TMP_Text caloriesText;
        [SerializeField] private Player player;

        private void Update()
        {
            int calories = CaloriesCounterSingleton.Instance.Calories;
            caloriesText.SetText($"Calories: {calories}");
        }

        public void BuyItem(GoodScriptable goodScriptable)
        {
            if (CaloriesCounterSingleton.Instance.Calories < goodScriptable.GoodPrice)
                return;

            CaloriesCounterSingleton.Instance.TakeCalories(goodScriptable.GoodPrice);
            player.ItemManager.EquipItem(goodScriptable.Item);
        }
    }

}
