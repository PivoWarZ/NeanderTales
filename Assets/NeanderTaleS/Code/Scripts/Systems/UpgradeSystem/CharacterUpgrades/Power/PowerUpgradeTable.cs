using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power
{
    [Serializable]
    public class PowerUpgradeTable
    {
        [SerializeField] private int _startPower;
        [SerializeField] private int _endPower;
        [SerializeField] private int[] _table;

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            var step = (_endPower - _startPower) / maxLevel;
            var table = new int[maxLevel];
            table[0] = _startPower;
            table[maxLevel-1] = _endPower;

            for (int level = 1; level < maxLevel - 1; level++)
            {
                var power = table[level-1] + step;
                table[level] = power;
            }
           
            _table = table;
        }
        
        public int GetPower(int level)
        {
            return _table[level-1];
        }
    }
}