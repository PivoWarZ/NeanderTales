using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.Spawner;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public class SceneInitializer: MonoBehaviour
    {
        [SerializeField] private SpawnerSettings _spawnSettings;
        [SerializeField] private Transform _playerStartTransform;
        private GameObject _player;
        private Spawner _spawner;

        [Inject]
        public void Construct(Spawner spawner, PlayerService service)
        {
            _spawner = spawner;
            _spawner.Initialize(_spawnSettings);
            
            _player = service.GetPlayer();
            _player.transform.position = _playerStartTransform.position;
        }

        [Button]
        public void Spawn()
        {
            _spawner.Spawn(_spawnSettings.SpawnPoints[0]);
        }
    }
}