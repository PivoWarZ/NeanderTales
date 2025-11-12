using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience;
using NeanderTaleS.Code.Scripts.Systems.Storages.Experience.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class ExperienceStorageInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ExperienceManager>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperienceStorage>()
                .AsCached()
                .NonLazy();
            
            //Bus
            
            Container.BindInterfacesAndSelfTo<LevelUpListener>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperiencePriceChangedObserver_SetExperience>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SpawnEnemyObserver_AddExperienceDealer>()
                .AsCached()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}