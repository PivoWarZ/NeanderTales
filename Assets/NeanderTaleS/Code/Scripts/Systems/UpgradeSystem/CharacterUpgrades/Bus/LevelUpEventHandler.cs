using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character.Bus
{
    public class LevelUpEventHandler: IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        private ICharacterUpgradeSystem _characterUpgradeSystem;

        public LevelUpEventHandler(IEventBus eventBus, ICharacterUpgradeSystem characterUpgradeSystem)
        {
            _eventBus = eventBus;
            _characterUpgradeSystem = characterUpgradeSystem;
        }
        
        void IInitializable.Initialize()
        {
            _characterUpgradeSystem.OnLevelUpEvent += LevelUpEvent;
        }

        void IDisposable.Dispose()
        {
            _characterUpgradeSystem.OnLevelUpEvent -= LevelUpEvent;
        }

        private void LevelUpEvent()
        {
            var requiredExperience = _characterUpgradeSystem.GetRequiredExperienceToNextLevel();
            _eventBus.RiseEvent(new LevelUpEvent(requiredExperience));
        }
    }
}