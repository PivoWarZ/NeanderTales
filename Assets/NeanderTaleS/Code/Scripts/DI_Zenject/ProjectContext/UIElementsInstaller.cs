using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Experience.LevelCounter;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience.Installer;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.LevelCounter;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Logo;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class UIElementsInstaller: MonoInstaller
    {
        [SerializeField] private HudUI _hudUI;
        [SerializeField] private Transform _container;
        public override void InstallBindings()
        {
            BindHudUI(_hudUI);
            
            BindLevelUpCounter();

            BindCharacterLogoInstaller();
            
            BindExperienceSliderInstaller();

            BindEnemyStateAdapter();

            BindEnemyTakeDamageObserverShowPopup();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindExperienceSliderInstaller()
        {
            Container.BindInterfacesAndSelfTo<ExperienceSliderInstaller>()
                .AsCached()
                .NonLazy();
        }

        private void BindCharacterLogoInstaller()
        {
            Container.BindInterfacesAndSelfTo<CharacterLogoInstaller>()
                .AsCached()
                .NonLazy();
        }

        private void BindLevelUpCounter()
        {
            Container.BindInterfacesAndSelfTo<LevelUpCounterAdapter>()
                .AsCached()
                .NonLazy();

            Container.Bind<ILevelUpCounter>().To<LevelUpCounter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindHudUI(HudUI ui)
        {
            var hud = Instantiate(ui, _container);
            HudUI hudUI = hud.GetComponent<HudUI>();
            
            Container.BindInstance(hudUI)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindEnemyStateAdapter()
        {
            Container.BindInterfacesAndSelfTo<EnemyStateAdapter>().AsSingle().NonLazy();
        }
        
        private void BindEnemyTakeDamageObserverShowPopup()
        {
            Container.BindInterfacesAndSelfTo<EnemyTakeDamageHandler_ShowPopup>()
                .AsCached()
                .NonLazy();
        }
    }
}