using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina
{
    [CreateAssetMenu(fileName = "StaminaUpgrade", menuName = "NeanderTaleS/UpgradeManager/Character/ New Stamina Upgrade")]
    public class StaminaUpgradeConfig: UpgradeConfig
    {
        public StaminaUpgradeTable StaminaTable;
        public override Upgrade Create()
        {
            return new StaminaUpgrade(this);
        }
        
        public int GetStamina(int level)
        {
            return StaminaTable.GetStamina(level);
        }
        
        public override void OnValidate()
        {
            StaminaTable.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel, 1);
        }
    }
}