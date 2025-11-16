using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.LevelCounter;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Logo;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class UIElementsInstaller: MonoInstaller
    {
        [SerializeField] HudUI _hudUI;
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
            
            
            DebugLogger.PrintBinding(this);
        }
        
        private void BindHudUI(HudUI ui)
        {
            var hud = Instantiate(ui);
            HudUI hudUI = hud.GetComponent<HudUI>();
            
            Container.BindInstance(hudUI)
                .AsSingle()
                .NonLazy();
        }
    }
}