using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    public class CaloriesCounterSingleton : Singleton<CaloriesCounterSingleton>
    {
        private int _calories;
        public int Calories => _calories;

        public void AddCalories(int calories) => _calories += calories;
        public void TakeCalories(int calories) => _calories -= calories;
    }
}
