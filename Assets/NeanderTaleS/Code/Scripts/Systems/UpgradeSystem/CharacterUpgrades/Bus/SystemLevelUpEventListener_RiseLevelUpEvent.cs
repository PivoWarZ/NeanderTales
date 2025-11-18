using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus
{
    public sealed class SystemLevelUpEventListener_RiseLevelUpEvent: IDisposable
    {
        private IEventBus _eventBus;
        private ICharacterUpgradeSystem _characterUpgradeSystem;

        public SystemLevelUpEventListener_RiseLevelUpEvent(IEventBus eventBus, ICharacterUpgradeSystem characterUpgradeSystem)
        {
            _eventBus = eventBus;
            _characterUpgradeSystem = characterUpgradeSystem;
            
            _characterUpgradeSystem.OnLevelUpEvent += LevelUpEvent;
        }

        void IDisposable.Dispose()
        {
            _characterUpgradeSystem.OnLevelUpEvent -= LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            _eventBus.RiseEvent(new LevelUpEvent(this.GetType().Name));
        }
    }
}