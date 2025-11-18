using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NeanderTaleS.Code.Scripts.Systems.Factory.Spawner
{
    public sealed class Spawner
    {
        public event Action<GameObject> OnSpawned;
        
        [Header("---------Spawner--------")]
        private readonly VelociraptorConfig _config;
        private readonly GameObject _prefab;
        private List<Transform> _spawnPoints = new ();
        private Transform _worldTransform;

        [Header("---------Level---------")] 
        [ Range(0, 10)] private int _level = 1;
        [Range(0.5f, 2f)] private float _minSizeValue = 0.5f;
        [Range(0.5f, 2f)] private float _maxSizeValue = 1f;
        private PlayerService _playerService;
        private GameObject _player;
        private IEventBus _eventBus;

        public Spawner(VelociraptorConfig config, GameObject prefab, PlayerService playerService, IEventBus eventBus)
        {
            _config = config;
            _prefab = prefab;
            _playerService = playerService;
            _eventBus = eventBus;
        }

        public void Initialize(SpawnerSettings settings)
        {
            _worldTransform = settings.WorldTransform;
            _spawnPoints = settings.SpawnPoints;
            _level = settings.Level;
            _minSizeValue = settings.MinSize;
            _maxSizeValue = settings.MaxSize;
            _player = _playerService.GetPlayer();
        }

        public void Spawn()
        {
            var random = Random.Range(0, _spawnPoints.Count);
            Vector3 randomPosition = _spawnPoints[random].position;
            
            var dino = GameObject.Instantiate(_prefab, randomPosition, Quaternion.identity);
            dino.transform.SetParent(_worldTransform);
            dino.gameObject.SetActive(false);
            
            dino.GetComponent<EnemyTargetComponent>().SetTarget(_player);
            
            var size = Random.Range(_minSizeValue, _maxSizeValue);
            _config.SetSize(size);
            
            dino.GetComponent<Enemy>().InitEnemy(_config);
            dino.gameObject.SetActive(true);
            
            OnSpawned?.Invoke(dino);
        }
        
        public void Spawn(Transform spawnPoint)
        {
            var dino = GameObject.Instantiate(_prefab, spawnPoint.position, Quaternion.identity);
            dino.transform.SetParent(_worldTransform);
            dino.gameObject.SetActive(false);

            dino.TryGetComponent<EnemyTargetComponent>(out EnemyTargetComponent targetComponent);

            if (targetComponent)
            {
                targetComponent.SetTarget(_player);
            }

            var size = Random.Range(_minSizeValue, _maxSizeValue);
            _config.SetSize(size);
            
            dino.GetComponent<Enemy>().InitEnemy(_config);
            dino.gameObject.SetActive(true);
            
            OnSpawned?.Invoke(dino);
            _eventBus.RiseEvent(new EnemySpawnedEvent(dino, GetType().Name));
        }
    }
}