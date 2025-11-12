using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class EventBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle().NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}