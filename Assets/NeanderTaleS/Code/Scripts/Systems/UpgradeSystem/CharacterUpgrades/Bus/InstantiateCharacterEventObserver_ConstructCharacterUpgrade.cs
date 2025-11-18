using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus
{
    public sealed class InstantiateCharacterEventObserver_ConstructCharacterUpgrade: IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly CharacterUpgrade _characterUpgrade;

        public InstantiateCharacterEventObserver_ConstructCharacterUpgrade(IEventBus eventBus, CharacterUpgrade characterUpgrade)
        {
            _eventBus = eventBus;
            _characterUpgrade = characterUpgrade;
            
            _eventBus.Subscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }
        
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<InstantiatePlayerEvent>(InitStatsUpgradeInstaller);
        }

        private void InitStatsUpgradeInstaller(InstantiatePlayerEvent @event)
        {
            _characterUpgrade.Construct(@event.Player);
        }
    }
}