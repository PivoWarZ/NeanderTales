using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class GameCycleInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCycleManager>()
                .AsSingle()
                .NonLazy();
            Container.BindInterfacesAndSelfTo<Systems.GameCycle.GameCycleInstaller>()
                .AsSingle()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
    }
}