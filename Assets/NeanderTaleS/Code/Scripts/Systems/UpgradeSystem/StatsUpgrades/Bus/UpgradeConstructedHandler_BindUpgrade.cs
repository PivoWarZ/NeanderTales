using System;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Stamina;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.EntryPoint;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Bus
{
    public class UpgradeConstructedHandler_BindUpgrade: IDisposable
    {
        private readonly DiContainer _context;
        private readonly StatsUpgradesSystemInitializer _upgradesSystemInitializer;

        public UpgradeConstructedHandler_BindUpgrade(DiContainer context, StatsUpgradesSystemInitializer upgradesSystemInitializer)
        {
            _context = context;
            _upgradesSystemInitializer = upgradesSystemInitializer;

            _upgradesSystemInitializer.OnUpgradeConstructed += BindUpgrade;
            _upgradesSystemInitializer.OnAllUpgradesConstructed += Unsubscribes;
        }

        private void Unsubscribes()
        {
            _upgradesSystemInitializer.OnUpgradeConstructed -= BindUpgrade;
            _upgradesSystemInitializer.OnAllUpgradesConstructed -= Unsubscribes;
        }

        private void BindUpgrade(Upgrade upgrade)
        {
            if (upgrade is HealthUpgrade healthUpgrade)
            {
                _context.BindInstance(healthUpgrade).AsSingle();
            }
            
            if (upgrade is PowerUpgrade powerUpgrade)
            {
                _context.BindInstance(powerUpgrade).AsSingle();
            }
            
            if (upgrade is StaminaUpgrade staminaUpgrade)
            {
                _context.BindInstance(staminaUpgrade).AsSingle();
            }
        }

        void IDisposable.Dispose()
        {
            Unsubscribes();
        }
    }
}