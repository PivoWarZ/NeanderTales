using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.Experience;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus
{
    public sealed class ExperienceStorageLevelUpListener_RiseLevelUpRequest: IInitializable, IDisposable
    {
        private ILevelUpRequest _levelUpRequest;
        private IEventBus _eventBus;

        public ExperienceStorageLevelUpListener_RiseLevelUpRequest(ILevelUpRequest levelUpRequest, IEventBus eventBus)
        {
            _levelUpRequest = levelUpRequest;
            _eventBus = eventBus;
        }
        
        void IInitializable.Initialize()
        {
            _levelUpRequest.OnLevelUpRequest += OnLevelUpRequest;
        }

        void IDisposable.Dispose()
        {
            _levelUpRequest.OnLevelUpRequest -= OnLevelUpRequest;
        }
        
        private void OnLevelUpRequest()
        {
            _eventBus.RiseEvent(new LevelUpRequest(this.GetType().Name));
        }
    }
}