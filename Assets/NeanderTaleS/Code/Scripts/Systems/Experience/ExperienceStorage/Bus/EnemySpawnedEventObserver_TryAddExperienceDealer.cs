using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus
{
    public sealed class EnemySpawnedEventObserver_TryAddExperienceDealer: IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly ExperienceManager _experienceManager;

        public EnemySpawnedEventObserver_TryAddExperienceDealer(IEventBus eventBus, ExperienceManager experienceManager)
        {
            _eventBus = eventBus;
            _experienceManager = experienceManager;
            
            _eventBus.Subscribe<EnemySpawnedEvent>(TryAddExperienceDealer);
        }

        private void TryAddExperienceDealer(EnemySpawnedEvent @event)
        {
            _experienceManager.TryAddExperienceDealer(@event.Enemy);
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<EnemySpawnedEvent>(TryAddExperienceDealer);
        }
    }
}