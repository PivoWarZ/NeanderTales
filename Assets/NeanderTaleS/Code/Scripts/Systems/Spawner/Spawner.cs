using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Configs;
using NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace NeanderTaleS.Code.Scripts.Systems.Spawner
{
    public class Spawner: MonoBehaviour
    {
        public event Action<GameObject> OnSpawned;
        
        [Header("---------Spawner--------")]
        [SerializeField] private VelociraptorConfig _config;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private Transform _worldTransform;
        [Space]
        [Header("---------Level---------")] 
        [SerializeField, Range(0.5f, 2f)] private float _minSizeValue = 0.5f;
        [SerializeField, Range(0.5f, 2f)] private float _maxSizeValue = 1f;
        [SerializeField] private GameObject _player;

        [Inject]
        public void Construct(SpawnPoints spawnPointses)
        {
            Debug.Log("Spawner Construct");
            _spawnPoints = spawnPointses.GetEnemySpawnPoints();
        }

        public void Initialize(PlayerService playerService)
        {
            _player = playerService.GetPlayer();
        }

        [Button]
        public void Spawn()
        {
            var random = Random.Range(0, _spawnPoints.Count);
            Vector3 randomPosition = _spawnPoints[random].position;
            
            var dino = Instantiate(_prefab, randomPosition, Quaternion.identity);
            dino.transform.SetParent(_worldTransform);
            dino.gameObject.SetActive(false);
            
            dino.GetComponent<EnemyTargetComponent>().SetTarget(_player);
            
            var size = Random.Range(_minSizeValue, _maxSizeValue);
            _config.SetSize(size);
            
            dino.GetComponent<Enemy>().InitEnemy(_config);
            dino.gameObject.SetActive(true);
        }
    }
}