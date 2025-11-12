using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Bus
{
    public class InstantiatePlayerObserver_InitializeStatsUpgradeInstaller: IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly StatsUpgradesInstaller _installer;

        public InstantiatePlayerObserver_InitializeStatsUpgradeInstaller(IEventBus eventBus, StatsUpgradesInstaller installer)
        {
            _eventBus = eventBus;
            _installer = installer;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }
        
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }

        private void InitStatsUpgradeInstaller(InstantiatePlayerEvent @event)
        {
            _installer.Initialize(@event.Player);
        }
    }
}