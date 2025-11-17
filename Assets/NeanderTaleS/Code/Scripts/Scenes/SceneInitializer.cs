using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.Factory;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using NeanderTaleS.Code.Scripts.Systems.Spawner;
using R3;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Scenes
{
    public sealed class SceneInitializer: MonoBehaviour
    {
        [SerializeField] private SpawnerSettings _spawnSettings;
        [SerializeField] private Transform _playerStartTransform;
        [SerializeField] private GameObject _exit;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private bool _isHome;
        private Spawner _spawner;
        private int _combatCounter;
        private readonly CompositeDisposable _combatDisposable = new ();

        [Inject]
        public void Construct(Spawner spawner, IPlayerCreator creator, PlayerService playerService, GameCycleManager gameCycle)
        {
            _spawner = spawner;
            _spawner.Initialize(_spawnSettings);
            
            creator.CreatePlayer(_playerStartTransform.position);
            playerService.GetPlayer().transform.SetParent(_worldTransform);
            gameCycle.StartGame();
        }

        private void Awake()
        {
//            _spawner.OnSpawned += Subscribe;
            _exit.SetActive(_isHome);
        }
        
        private void Start()
        {
            if (_spawnSettings.SpawnPoints.Count == 0)
            {
                return;
            }

            foreach (var spawnPoint in _spawnSettings.SpawnPoints)
            {
                _spawner.Spawn(spawnPoint);
                _combatCounter++;
            }
        }

        private void Subscribe(GameObject enemy)
        {
           var dispose = enemy.GetComponent<IHitPointsComponent>().CurrentHitPoints
                .Where(hp => hp <= 0)
                .Subscribe(OnEnemyDie);
           _combatDisposable.Add(dispose);
        }

        private void OnEnemyDie(float _)
        {
            _combatCounter--;

            if (_combatCounter <= 0)
            {
                _exit.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _spawner.OnSpawned -= Subscribe;
            _combatDisposable.Dispose();
        }
    }
}