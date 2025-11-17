using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Bus;
using NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class ExperienceStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ExperienceManager>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperienceStorage>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CharacterLevelHandler_SetLevelUpCounter>()
                .AsCached()
                .NonLazy();
            
            //Bus
            
            Container.BindInterfacesAndSelfTo<ExperienceStorageLevelUpListener_RiseLevelUpRequest>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperiencePriceChangedObserver_SetRequiredExperience>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SpawnEnemyObserver_AddExperienceDealer>()
                .AsCached()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}