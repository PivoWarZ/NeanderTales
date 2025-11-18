using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.Bus
{
    public class EnemySpawnedEventObserver_AddDamageable: IDisposable
    {
        private IEventBus _eventBus;
        private EnemyTakeDamageHandler_ShowPopup _handler;

        public EnemySpawnedEventObserver_AddDamageable(IEventBus eventBus, EnemyTakeDamageHandler_ShowPopup handler)
        {
            _eventBus = eventBus;
            _handler = handler;
            
            _eventBus.Subscribe<EnemySpawnedEvent>(AddDamageble);
        }

        private void AddDamageble(EnemySpawnedEvent enemy)
        {
            enemy.Enemy.TryGetComponent<ITakeDamageEvents>(out ITakeDamageEvents damageable);

            if (damageable != null)
            {
                _handler.AddDamageable(damageable);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<EnemySpawnedEvent>(AddDamageble);
        }
    }
}