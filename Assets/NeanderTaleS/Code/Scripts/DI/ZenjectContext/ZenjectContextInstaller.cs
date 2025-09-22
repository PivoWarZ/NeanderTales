using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using NeanderTaleS.Code.Scripts.Systems.Experience;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class ZenjectContextInstaller: MonoInstaller
    {
        [SerializeField] private ExperienceRewardComponent experienceRewardComponent;
        [SerializeField] GameObject _player;
        [SerializeField] HudUI _hudUI;
        public override void InstallBindings()
        {
            BindPlayerService(_player);
            _player.GetComponent<Player>().Init();

            BindExperienceSystem();
            
            Container.BindInstance(_hudUI);
            Container.BindInterfacesAndSelfTo<PlayerStatsInstaller>().AsSingle().NonLazy();
        }

        private void BindExperienceSystem()
        {
            Container.Bind<IExperienceStorage>().To<ExperienceStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ExperienceSystem>().AsSingle().WithArguments(experienceRewardComponent).NonLazy();
        }

        private void BindPlayerService(GameObject player)
        {
            Container.Bind<PlayerService>().AsSingle().WithArguments(player).NonLazy();
        }
    }
}