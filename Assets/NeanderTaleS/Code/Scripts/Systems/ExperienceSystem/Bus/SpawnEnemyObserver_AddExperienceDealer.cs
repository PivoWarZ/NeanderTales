using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public class SpawnEnemyObserver_AddExperienceDealer: IInitializable, IDisposable
    {
        private ExperienceManager _experienceManager;
        private IEventBus _eventBus;

        public SpawnEnemyObserver_AddExperienceDealer(ExperienceManager experienceManager, IEventBus eventBus)
        {
            _experienceManager = experienceManager;
            _eventBus = eventBus;
        }

        void IInitializable.Initialize()
        {
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