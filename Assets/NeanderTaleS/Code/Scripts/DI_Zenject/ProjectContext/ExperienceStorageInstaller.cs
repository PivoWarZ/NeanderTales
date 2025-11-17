using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus;
using NeanderTaleS.Code.Scripts.Systems.Experience.LevelCounter.Bus;
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
            
            Container.BindInterfacesAndSelfTo<CharacterLevelHandler_SetRequiredExperience>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SpawnEnemyObserver_AddExperienceDealer>()
                .AsCached()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}