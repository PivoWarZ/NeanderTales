using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Experience;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public class LevelUpListener: IInitializable, IDisposable
    {
        private ILevelUpRequest _levelUpRequest;
        private IEventBus _eventBus;

        public LevelUpListener(ILevelUpRequest levelUpRequest, IEventBus eventBus)
        {
            _levelUpRequest = levelUpRequest;
            _eventBus = eventBus;
        }
        
        void IInitializable.Initialize()
        {
            _levelUpRequest.OnLevelUpRequest += OnLevelUpRequest;
        }

        private void OnLevelUpRequest()
        {
            _eventBus.RiseEvent(new LevelUpRequest());
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}