using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health
{
    [Serializable]
    public class HealthUpgradeTable
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private int _endHealth;
        [SerializeField] private int[] _table;

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            var step = (_endHealth - _startHealth) / maxLevel;
            if (step <= 0) step = 1;
            var table = new int[maxLevel];
            table[0] = _startHealth;
            table[maxLevel-1] = _endHealth;

            for (int level = 1; level < maxLevel - 1; level++)
            {
                var health = table[level-1] + step;
                table[level] = health;
            }
           
            _table = table;
        }
        
        public int GetHealth(int level)
        {
            return _table[level-1];
        }
    }
}