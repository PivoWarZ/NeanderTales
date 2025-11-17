using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Bus
{
    public sealed class ExperiencePriceChangedObserver_SetRequiredExperience: IDisposable
    {
        private IExperienceSetter _experienceSetter;
        private IEventBus _eventBus;

        public ExperiencePriceChangedObserver_SetRequiredExperience(IExperienceSetter experienceSetter, IEventBus eventBus)
        {
            _experienceSetter = experienceSetter;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<ExperiencePriceChangedEvent>(SetExperienceValues);
        }

        private void SetExperienceValues(ExperiencePriceChangedEvent @event)
        {
            var requiredExperience = @event.RequiredExperience;
            _experienceSetter.SetRequiredExperience(requiredExperience);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<ExperiencePriceChangedEvent>(SetExperienceValues);
        }
    }
}