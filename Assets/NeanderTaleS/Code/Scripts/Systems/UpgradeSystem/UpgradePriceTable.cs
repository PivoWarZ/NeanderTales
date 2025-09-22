using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem
{
    [Serializable]
    public class UpgradePriceTable
    {
        [Space]
        [SerializeField] private int _basePrice;
        
        [Space]
        [SerializeField] private int[] _levelsPrice;
        public int GetNextPrice(int level)
        {
            var index = level - 1;
            index = Mathf.Clamp(index, 0, _levelsPrice.Length - 1);
            return _levelsPrice[index];
        }

        public void OnValidate(int maxLevel)
        {
            EvaluatePriceTable(maxLevel);
        }

        public void OnValidate(int maxLevel, int value)
        {
            EvaluatePriceTable(maxLevel, value);
        }

        private void EvaluatePriceTable(int maxLevel)
        {
            var table = new int[maxLevel];
            table[0] = default;
            for (int level = 2; level <= maxLevel; level++)
            {
                table[level-1] = _basePrice * level;
            }
            
            _levelsPrice = table;
        }

        private void EvaluatePriceTable(int maxLevel, int value)
        {
            var table = new int[maxLevel];
            table[0] = default;
            
            for (int level = 2; level <= maxLevel; level++)
            {
                table[level-1] = value;
            }
            
            _levelsPrice = table;
        }
    }
}