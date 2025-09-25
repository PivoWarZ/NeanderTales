using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class GameCycleInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameCycleManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Systems.GameCycle.GameCycleInstaller>().AsSingle();
            
            Debug.Log($"Binding {GetType().Name}");
        }
    }
}