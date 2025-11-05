using NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.Factory;
using NeanderTaleS.Code.Scripts.Systems.Spawner;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class ZenjectContextInstaller: MonoInstaller
    {
        [SerializeField] private HudUI _hudUI;
        [SerializeField] private Transform _container;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _sinematicCamera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private VelociraptorConfig _dinoConfig;
        [SerializeField] private GameObject _dinoPrefab;
        
        public override void InstallBindings()
        {
            BindPlayerInstaller();
            
            InstantiateCamera();
            
            InstantiateEventSystem();
            
            BindHudUI();
            
            BindSpawner();
            
            BindEnemyStateAdapter();
            
            BindEnemyTakeDamageObserver_ShowPopup();
            
            BindEnemySpawnedHandler();

            BindPlayerService();

            
            Debug.Log($"Binding {GetType().Name}");
        }

        private void BindPlayerInstaller()
        {
            Container.BindInterfacesAndSelfTo<PlayerInstaller>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemySpawnedHandler()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnedHandler>().AsSingle().NonLazy();
        }

        private void BindEnemyTakeDamageObserver_ShowPopup()
        {
            Container.BindInterfacesAndSelfTo<EnemyTakeDamageObserver_ShowPopup>().AsSingle().NonLazy();
        }

        private void BindEnemyStateAdapter()
        {
            Container.BindInterfacesAndSelfTo<EnemyStateAdapter>().AsSingle().NonLazy();
        }

        private void BindSpawner()
        {
            Container.Bind<Spawner>()
                .AsSingle()
                .WithArguments(_dinoConfig, _dinoPrefab)
                .NonLazy();
        }

        private void InstantiateEventSystem()
        {
            Instantiate(_eventSystem, _container);
        }

        private void InstantiateCamera()
        {
            var mainCamera = Instantiate(_camera, _container);
            var cinemashine = Instantiate(_sinematicCamera, _container);
            
            Container.BindInterfacesAndSelfTo<CamerasProvider>()
                .AsSingle()
                .WithArguments(mainCamera, cinemashine)
                .NonLazy();
        }

        private void BindHudUI()
        {
            var hud = Instantiate(_hudUI, _container);
            HudUI hudUI = hud.GetComponent<HudUI>();
            
            Container.BindInstance(hudUI)
                .AsSingle()
                .NonLazy();

            EnemyStateView enemyview = hud.EnemyStatesView;
            Container.BindInstance(enemyview);
        }

        private void BindPlayerService()
        {
            Container.BindInterfacesAndSelfTo<PlayerService>()
                .AsSingle()
                .NonLazy();
        }
    }
}