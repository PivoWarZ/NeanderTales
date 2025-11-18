using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus
{
    public sealed class SpawnEnemyObserver_AddExperienceDealer: IDisposable
    {
        private IExperienceManager _experienceManager;
        private IEventBus _eventBus;

        public SpawnEnemyObserver_AddExperienceDealer(IExperienceManager experienceManager, IEventBus eventBus)
        {
            _experienceManager = experienceManager;
            _eventBus = eventBus;
            
            _eventBus.Subscribe<SpawnEnemyEvent>(TryAddExperienceDealer);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SpawnEnemyEvent>(TryAddExperienceDealer);
        }

        private void TryAddExperienceDealer(SpawnEnemyEvent @event)
        {
            var enemy = @event.Enemy;
            _experienceManager.TryAddExperienceDealer(enemy);
        }
    }
}