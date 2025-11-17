using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter;
using NeanderTaleS.Code.Scripts.UI;
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
            
            Container.BindInterfacesAndSelfTo<LevelUpCounterAdapter>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CharacterLogoInstaller>()
                .AsCached()
                .NonLazy();
            
            Container.Bind<ILevelUpCounter>().To<LevelUpCounter>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperienceSliderInstaller>()
                .AsCached()
                .NonLazy();
            
            
            DebugLogger.PrintBinding(this);
        }
        
        private void BindHudUI(HudUI ui)
        {
            var hud = Instantiate(ui, _container);
            HudUI hudUI = hud.GetComponent<HudUI>();
            
            Container.BindInstance(hudUI)
                .AsSingle()
                .NonLazy();
        }
    }
}