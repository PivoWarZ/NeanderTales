using NeanderTaleS.Code.Scripts.Systems.EventBus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class EventBusInstaller: MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle().NonLazy();
        }
    }
}