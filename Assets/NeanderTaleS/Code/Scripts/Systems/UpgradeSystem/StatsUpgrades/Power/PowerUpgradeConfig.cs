using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Power
{
    [CreateAssetMenu(fileName = "PowerUpgrade", menuName = "NeanderTaleS/UpgradeManager/Character/ New Power Upgrade")]
    public class PowerUpgradeConfig: UpgradeConfig
    {
        public PowerUpgradeTable PowerTable;
        public override Upgrade Create()
        {
            return new PowerUpgrade(this);
        }
        
        public int GetPower(int level)
        {
            return PowerTable.GetPower(level);
        }
        
        public override void OnValidate()
        {
            PowerTable.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel, 1);
        }
    }
}