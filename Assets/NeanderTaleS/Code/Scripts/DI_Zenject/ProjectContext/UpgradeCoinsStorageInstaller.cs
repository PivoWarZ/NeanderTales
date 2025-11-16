using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins;
using NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class UpgradeCoinsStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUpgradeCoinsStorage>()
                .To<UpgradeCoinsStorage>()
                .AsSingle()
                .NonLazy(); 
            
            // Bus
            
            Container.BindInterfacesAndSelfTo<LevelUpEventListener_AddRewardCoins>()
                .AsSingle()
                .NonLazy();
            
            BindStarsCountAdapter();
            
            DebugLogger.PrintBinding(this);
        }
        
        private void BindStarsCountAdapter()
        {
            Container.BindInterfacesAndSelfTo<CoinsCountAdapter>().AsSingle().NonLazy();
        }
    }
}