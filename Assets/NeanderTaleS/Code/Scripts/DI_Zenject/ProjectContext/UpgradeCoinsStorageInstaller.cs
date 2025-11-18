using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage;
using NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class UpgradeCoinsStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUpgradeCoinsStorage(); 
            
            // Bus
            
            BindCharacterLevelListener();
            
            BindStarsCountAdapter();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindCharacterLevelListener()
        {
            Container.BindInterfacesAndSelfTo<CharacterLevelListener_AddRewardCoins>()
                .AsCached()
                .NonLazy();
        }

        private void BindUpgradeCoinsStorage()
        {
            Container.Bind<IUpgradeCoinsStorage>()
                .To<UpgradeCoinsStorage>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStarsCountAdapter()
        {
            Container.BindInterfacesAndSelfTo<CoinsCountAdapter>().AsSingle().NonLazy();
        }
    }
}