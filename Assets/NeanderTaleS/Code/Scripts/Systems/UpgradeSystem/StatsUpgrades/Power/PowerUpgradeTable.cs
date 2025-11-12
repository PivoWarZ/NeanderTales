using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power
{
    [Serializable]
    public class PowerUpgradeTable
    {
        [SerializeField] private int _startValue;
        [SerializeField] private int _endValue;
        [SerializeField] private int _step;
        [SerializeField] private bool _isStep;
        [SerializeField] private int[] _table;

        public void OnValidate(int maxLevel)
        {
            if (_isStep)
            {
                EvaluateStepTable(maxLevel, _step);
            }
            else
            {
                EvaluateTable(maxLevel);
            }
        }

        private void EvaluateStepTable(int maxLevel, int step)
        {
            var table = new int[maxLevel];
            table[0] = default;
            table[maxLevel-1] = step;

            for (int level = 1; level < maxLevel - 1; level++)
            {
                var value = step;
                table[level] = value;
            }
           
            _table = table;
        }
        
        private void EvaluateTable(int maxLevel)
        {
            var step = (_endValue - _startValue) / maxLevel;
            if (step <= 0) step = 1;
            var table = new int[maxLevel];
            table[0] = _startValue;
            table[maxLevel-1] = _endValue;

            for (int level = 1; level < maxLevel - 1; level++)
            {
                var value = table[level-1] + step;
                table[level] = value;
            }
           
            _table = table;
        }
        
        public int GetPower(int level)
        {
            return _table[level-1];
        }
    }
}