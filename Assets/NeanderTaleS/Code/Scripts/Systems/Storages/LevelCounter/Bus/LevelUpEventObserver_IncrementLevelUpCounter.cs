using System;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter.Bus
{
    public class LevelUpEventObserver_IncrementLevelUpCounter: IDisposable
    {
        private IEventBus _eventBus;
        private ILevelUpCounter _levelUpCounter;

        public LevelUpEventObserver_IncrementLevelUpCounter(IEventBus eventBus, ILevelUpCounter levelUpCounter)
        {
            _eventBus = eventBus;
            _levelUpCounter = levelUpCounter;
            
            _eventBus.Subscribe<LevelUpEvent>(IncrementLevelUpCounter);
        }

        private void IncrementLevelUpCounter(LevelUpEvent _)
        {
            _levelUpCounter.LevelUp();
        }

        void IDisposable.Dispose()
        {
            _eventBus.Unsubscribe<LevelUpEvent>(IncrementLevelUpCounter);
            _levelUpCounter.Dispose();
        }
    }
}