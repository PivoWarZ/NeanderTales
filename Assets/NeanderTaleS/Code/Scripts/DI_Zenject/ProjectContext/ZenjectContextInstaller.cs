using NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Factory;
using NeanderTaleS.Code.Scripts.Systems.Factory.Spawner;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.Bus;
using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class ZenjectContextInstaller: MonoInstaller
    {
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
            
            BindSpawner();
            
            BindEnemyStateAdapter();
            
            BindEnemyTakeDamageObserver_ShowPopup();

            BindPlayerService();
            
            Container.BindInterfacesAndSelfTo<InstantiatePlayerEventObserver_InitializeCamerasProvider>()
                .AsSingle()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindPlayerInstaller()
        {
            Container.BindInterfacesAndSelfTo<PlayerInstaller>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyTakeDamageObserver_ShowPopup()
        {
            Container.BindInterfacesAndSelfTo<EnemyTakeDamageHandler_ShowPopup>().AsCached().NonLazy();
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

        private void BindPlayerService()
        {
            Container.Bind<PlayerService>()
                .AsSingle()
                .NonLazy();
        }
    }
}