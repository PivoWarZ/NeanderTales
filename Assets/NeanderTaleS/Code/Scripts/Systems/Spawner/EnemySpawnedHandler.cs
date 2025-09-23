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
        private Spawner _spawner;
        private EnemyTakeDamageObserver _takeDamageObserver;
        private ExperienceSystem _experienceSystem;

        public EnemySpawnedHandler(Spawner spawner, EnemyTakeDamageObserver takeDamageObserver, ExperienceSystem experienceSystem)
        {
            _spawner = spawner;
            _takeDamageObserver = takeDamageObserver;
            _experienceSystem = experienceSystem;
        }
        
        public void Initialize()
        {
            _spawner.OnSpawned += OnSpawn;
        }

        private void OnSpawn(GameObject enemy)
        {
            bool isTakeDamageable = enemy.TryGetComponent<ITakeDamageable>(out var takeDamageable);

            if (isTakeDamageable)
            {
                _takeDamageObserver.AddDamageable(takeDamageable);
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