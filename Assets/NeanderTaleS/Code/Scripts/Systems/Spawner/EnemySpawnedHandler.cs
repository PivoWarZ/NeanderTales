using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Spawner
{
    public class EnemySpawnedHandler: IInitializable, IDisposable
    {
        private readonly Spawner _spawner;
        private readonly EnemyTakeDamageObserver_ShowPopup _takeDamageObserverShowPopup;
        private readonly ExperienceSystem _experienceSystem;

        public EnemySpawnedHandler(Spawner spawner, EnemyTakeDamageObserver_ShowPopup takeDamageObserverShowPopup, ExperienceSystem experienceSystem)
        {
            _spawner = spawner;
            _takeDamageObserverShowPopup = takeDamageObserverShowPopup;
            _experienceSystem = experienceSystem;
        }
        
        public void Initialize()
        {
            _spawner.OnSpawned += OnSpawn;
        }

        private void OnSpawn(GameObject enemy)
        {
            bool isTakeDamageable = enemy.TryGetComponent<ITakeDamageEvents>(out var takeDamageable);

            if (isTakeDamageable)
            {
                _takeDamageObserverShowPopup.AddDamageable(takeDamageable);
            }

            bool isExpDealer = enemy.TryGetComponent<IExperienceDealer>(out var dealer);

            if (isExpDealer)
            {
                _experienceSystem.AddExperienceDealer(dealer);
            }
        }

        public void Dispose()
        {
            _spawner.OnSpawned -= OnSpawn;
        }
    }
}