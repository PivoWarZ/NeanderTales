using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.Experience;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class ZenjectContextInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private HudUI _hudUI;
        [SerializeField] private Transform _container;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _sinematicCamera;
        [SerializeField] private EventSystem _eventSystem;
        
        public override void InstallBindings()
        {
            var player = BindPlayerService(_player);
            
            InstantiateCamera(player);
            
            InstantiateEventSystem();

            BindExperienceSystem();
            
            BindHudUI();


            Container.BindInterfacesAndSelfTo<PlayerStatsInstaller>().AsSingle().NonLazy();
        }

        private void InstantiateEventSystem()
        {
            Instantiate(_eventSystem, _container);
        }

        private void InstantiateCamera(GameObject player)
        {
            var mainCamera = Instantiate(_camera, _container);
            var cinemashine = Instantiate(_sinematicCamera, _container);
            cinemashine.GetComponent<CinemachineCamera>().Follow = player.transform;
        }

        private void BindHudUI()
        {
            var hud = Instantiate(_hudUI, _container);
            HudUI hudUi = hud.GetComponent<HudUI>();
            
            Container.BindInstance(hudUi)
                .AsSingle()
                .NonLazy();
        }

        private void BindExperienceSystem()
        {
            Container.Bind<IExperienceStorage>().To<ExperienceStorage>().AsSingle().NonLazy();
        }

        private GameObject BindPlayerService(GameObject player)
        {
            var entity = Instantiate(player, _container);
            Container.Bind<PlayerService>().AsSingle().WithArguments(entity).NonLazy();
            entity.GetComponent<Player>().Init();
            
            return entity;
        }
    }
}