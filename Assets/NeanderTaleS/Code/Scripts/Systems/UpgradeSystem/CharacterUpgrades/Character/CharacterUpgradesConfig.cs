using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    [CreateAssetMenu(fileName = "CharacterUpgrades", menuName = "NeanderTaleS/UpgradeManager/Character/ New Character Upgrades")]
    public class CharacterUpgradesConfig: UpgradeConfig
    {
        public HealthUpgradeTable HealthTable;
        public StaminaUpgradeTable StaminaTable;
        public PowerUpgradeTable PowerTable;
        
        public override Upgrade Create()
        {
            return new CharacterUpgrade(this);
        }

        public int GetHealth(int level)
        {
            return HealthTable.GetHealth(level);
        }
        
        public int GetStamina(int level)
        {
            return StaminaTable.GetStamina(level);
        }
        
        public int GetPower(int level)
        {
            return PowerTable.GetPower(level);
        }

        public override void OnValidate()
        {
            HealthTable.OnValidate(MaxLevel);
            StaminaTable.OnValidate(MaxLevel);
            PowerTable.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel);
        }
    }
}