using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.EntryPoint;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Bus
{
    public sealed class InstantiatePlayerObserver_InitializeStatsUpgradeInstaller: IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly StatsUpgradesSystemInitializer _initializer;

        public InstantiatePlayerObserver_InitializeStatsUpgradeInstaller(IEventBus eventBus, StatsUpgradesSystemInitializer initializer)
        {
            _eventBus = eventBus;
            _initializer = initializer;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }
        
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }

        private void InitStatsUpgradeInstaller(InstantiatePlayerEvent @event)
        {
            _initializer.Initialize(@event.Player);
        }
    }
}