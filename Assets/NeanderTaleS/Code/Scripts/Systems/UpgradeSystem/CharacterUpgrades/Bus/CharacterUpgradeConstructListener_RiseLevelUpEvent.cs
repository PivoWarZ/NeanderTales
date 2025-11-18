using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus
{
    public sealed class CharacterUpgradeConstructListener_RiseLevelUpEvent: IDisposable
    {
        private CharacterUpgrade _characterUpgrade;
        private IEventBus _eventBus;

        public CharacterUpgradeConstructListener_RiseLevelUpEvent(CharacterUpgrade characterUpgrade, IEventBus eventBus)
        {
            _characterUpgrade = characterUpgrade;
            _eventBus = eventBus;

            _characterUpgrade.OnCharacterUpgradeConstructed += OnLevelUpEvent;
        }

        private void OnLevelUpEvent()
        {
            _eventBus.RiseEvent(new LevelUpEvent(this.GetType().Name));
        }

        void IDisposable.Dispose()
        {
            _characterUpgrade.OnCharacterUpgradeConstructed -= OnLevelUpEvent;
        }
    }
}