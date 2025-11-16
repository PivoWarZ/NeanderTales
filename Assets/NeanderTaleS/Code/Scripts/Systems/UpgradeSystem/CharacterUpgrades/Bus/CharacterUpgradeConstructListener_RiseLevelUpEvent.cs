using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using UnityEngine;

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
            float elapsedTime = Time.realtimeSinceStartup;
        }

        void IDisposable.Dispose()
        {
            _characterUpgrade.OnCharacterUpgradeConstructed -= OnLevelUpEvent;
        }
    }
}