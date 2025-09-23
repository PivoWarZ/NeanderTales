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
        
        public Transform WorldTransform;
        
        [Header("---------Spawner--------")]
        [SerializeField] private VelociraptorConfig _config;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private List<Transform> _spawnPoints;
        [Space]
        [Header("---------Level---------")] 
        [SerializeField, Range(0, 10)] private int level = 1;
        [SerializeField, Range(0.5f, 2f)] private float _minSizeValue = 0.5f;
        [SerializeField, Range(0.5f, 2f)] private float _maxSizeValue = 1f;
        [SerializeField] private GameObject _player;

        public void Initialize(PlayerService playerService)
        {
            _player = playerService.GetPlayer();
        }
        
        public void Spawn()
        {
            var random = Random.Range(0, _spawnPoints.Count);
            Vector3 randomPosition = _spawnPoints[random].position;
            
            var dino = Instantiate(_prefab, randomPosition, Quaternion.identity);
            dino.transform.SetParent(WorldTransform);
            dino.gameObject.SetActive(false);
            
            dino.GetComponent<EnemyTargetComponent>().SetTarget(_player);
            
            var size = Random.Range(_minSizeValue, _maxSizeValue);
            _config.SetSize(size);
            
            dino.GetComponent<Enemy>().InitEnemy(_config);
            dino.gameObject.SetActive(true);
        }
        
        public void Spawn(Transform spawnPoint)
        {
            var dino = Instantiate(_prefab, spawnPoint.position, Quaternion.identity);
            dino.transform.SetParent(WorldTransform);
            dino.gameObject.SetActive(false);
            
            dino.GetComponent<EnemyTargetComponent>().SetTarget(_player);
            
            var size = Random.Range(_minSizeValue, _maxSizeValue);
            _config.SetSize(size);
            
            dino.GetComponent<Enemy>().InitEnemy(_config);
            dino.gameObject.SetActive(true);
        }

        public void SetRange(float min, float max)
        {
            _minSizeValue = min;
            _maxSizeValue = max;
        }
    }
}