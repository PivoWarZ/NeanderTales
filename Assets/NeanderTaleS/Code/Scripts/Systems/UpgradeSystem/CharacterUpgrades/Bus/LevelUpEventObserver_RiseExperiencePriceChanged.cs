using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus
{
    public class LevelUpEventObserver_RiseExperiencePriceChanged: IDisposable
    {
        private ICharacterUpgradeSystem _upgradeSystem;
        private IEventBus _eventBus;

        public LevelUpEventObserver_RiseExperiencePriceChanged(ICharacterUpgradeSystem upgradeSystem, IEventBus eventBus)
        {
            _upgradeSystem = upgradeSystem;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<LevelUpEvent>(RiseExperiencePriceChanged);
        }
        
        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<LevelUpEvent>(RiseExperiencePriceChanged);
        }

        private void RiseExperiencePriceChanged(LevelUpEvent @event)
        {
            var requiredExperience = _upgradeSystem.GetRequiredExperienceToNextLevel();
            _eventBus.RiseEvent(new ExperiencePriceChangedEvent(requiredExperience, this.GetType().Name));
        }
    }
}