using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.InputSystems;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.AttackInput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Bus;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.MoveInput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class InputSystemsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAttackInput();
            BindJumpInput();
            BindRotateInput();
            BindMoveInput();
            BindInputInitializer();
            
            DebugLogger.PrintBinding(this);
        }
        
        private void BindAttackInput()
        {

            Container.BindInterfacesAndSelfTo<AttackInputListener>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<AttackInputController>()
                .AsCached()
                .NonLazy();
        }

        private void BindJumpInput()
        {
            Container.BindInterfacesAndSelfTo<JumpInputListener>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<JumpInputController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRotateInput()
        {
            Container.BindInterfacesAndSelfTo<CursorPositionListener>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<RotateController>()
                .AsCached()
                .NonLazy();
        }

        private void BindMoveInput()
        {
            Container.BindInterfacesAndSelfTo<MoveInputListener>()
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<MoveInputController>()
                .AsCached()
                .NonLazy();
        }
        
        private void BindInputInitializer()
        {
            Container.Bind<InputSystemInitializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}