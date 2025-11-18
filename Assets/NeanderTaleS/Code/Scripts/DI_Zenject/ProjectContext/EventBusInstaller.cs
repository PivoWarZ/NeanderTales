using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus;
using NeanderTaleS.Code.Scripts.UI.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class EventBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EnemySpawnedEventObserver_AddDamageable>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<EnemySpawnedEventObserver_TryAddExperienceDealer>()
                .AsCached()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}