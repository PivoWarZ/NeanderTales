using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    [Serializable]
    public class StaminaUpgradeTable
    {
        [SerializeField] private int _startStamina;
        [SerializeField] private int _endStamina;
        [SerializeField] private int[] _table;

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            var step = (_endStamina - _startStamina) / maxLevel;
            var table = new int[maxLevel];
            table[0] = _startStamina;
            table[maxLevel-1] = _endStamina;

            for (int level = 1; level < maxLevel - 1; level++)
            {
                var stamina = table[level-1] + step;
                table[level] = stamina;
            }
           
            _table = table;
        }
        
        public int GetStamina(int level)
        {
            return _table[level-1];
        }
    }
}