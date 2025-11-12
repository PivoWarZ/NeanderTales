using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Bus
{
    public class ExperiencePriceChangedObserver_SetExperience: IDisposable
    {
        private IExperienceSetter _experienceSetter;
        private IEventBus _eventBus;

        public ExperiencePriceChangedObserver_SetExperience(IExperienceSetter experienceSetter, IEventBus eventBus)
        {
            _experienceSetter = experienceSetter;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<ExperiencePriceChangedEvent>(SetExperienceValues);
        }

        private void SetExperienceValues(ExperiencePriceChangedEvent @event)
        {
            var requiredExperience = @event.RequiredExperience;
            _experienceSetter.SetExperience(0, requiredExperience);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<ExperiencePriceChangedEvent>(SetExperienceValues);
        }
    }
}