using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus
{
    public class LevelUpRequestObserver_CanLevelUp: IInitializable, IDisposable
    {
        private IEventBus _eventBus;
        private ICharacterUpgradeSystem _characterUpgradeSystem;

        public LevelUpRequestObserver_CanLevelUp(IEventBus eventBus, ICharacterUpgradeSystem characterUpgradeSystem)
        {
            _eventBus = eventBus;
            _characterUpgradeSystem = characterUpgradeSystem;
        }

        void IInitializable.Initialize()
        {
            _eventBus.Subscribe<LevelUpRequest>(LevelUpRequest);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<LevelUpRequest>(LevelUpRequest);
        }

        private void LevelUpRequest(LevelUpRequest _)
        {
            _characterUpgradeSystem.CanLevelUp();
        }
    }
}