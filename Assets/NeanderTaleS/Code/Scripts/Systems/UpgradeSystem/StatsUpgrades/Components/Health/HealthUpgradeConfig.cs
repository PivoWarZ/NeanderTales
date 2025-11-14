using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Health
{
    [CreateAssetMenu(fileName = "HealthUpgrade", menuName = "NeanderTaleS/UpgradeManager/Character/ New Health Upgrade")]
    public class HealthUpgradeConfig: UpgradeConfig
    {
        public HealthUpgradeTable HealthTable;
        public override Upgrade Create()
        {
            return new HealthUpgrade(this);
        }

        public int GetHealth(int level)
        {
            return HealthTable.GetHealth(level);
        }
        
        public override void OnValidate()
        {
            HealthTable.OnValidate(MaxLevel);
            PriceTable.OnValidate(MaxLevel, 1);
        }
    }
}