using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Stamina;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Installer
{
    public class StatsUpgradeSystemInstaller: IDisposable
    {
        private readonly List<Upgrade> _upgrades = new ();

        public List<Upgrade> Upgrades => _upgrades;

        public void Install()
        {
            HealthUpgradeConfig healthConfig = Resources.Load<HealthUpgradeConfig>("Upgrades/HealthUpgrade");
            HealthUpgrade healthUpgrade = new HealthUpgrade(healthConfig);
            _upgrades.Add(healthUpgrade);
            
            PowerUpgradeConfig powerConfig = Resources.Load<PowerUpgradeConfig>("Upgrades/PowerUpgrade");
            PowerUpgrade powerUpgrade = new PowerUpgrade(powerConfig);
            _upgrades.Add(powerUpgrade);
            
            StaminaUpgradeConfig staminaConfig = Resources.Load<StaminaUpgradeConfig>("Upgrades/StaminaUpgrade");
            StaminaUpgrade staminaUpgrade = new StaminaUpgrade(staminaConfig);
            _upgrades.Add(staminaUpgrade);
            
            Debug.Log($"Upgrades installed: {healthConfig}, {powerConfig}, {staminaConfig}");
        }

        public void Dispose()
        {
            _upgrades.Clear();
        }
    }
}