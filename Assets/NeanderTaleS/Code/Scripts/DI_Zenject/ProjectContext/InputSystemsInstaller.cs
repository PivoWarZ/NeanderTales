using NeanderTaleS.Code.Scripts.Systems.InputSystems;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.AttackInput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.MoveInput;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class InputSystemsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAttackInput();
            BindJumpInput();
            BindRotateInput();
            BindMoveInput();
            BindInputInitializer();
            
            Debug.Log($"Binding {GetType().Name}");
        }
        
        private void BindAttackInput()
        {

            Container.BindInterfacesAndSelfTo<AttackInputListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<AttackInputController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindJumpInput()
        {
            Container.BindInterfacesAndSelfTo<JumpInputListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<JumpInputController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRotateInput()
        {
            Container.BindInterfacesAndSelfTo<CursorPositionListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<RotateController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMoveInput()
        {
            Container.BindInterfacesAndSelfTo<MoveInputListener>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<MoveInputController>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindInputInitializer()
        {
            Container.BindInterfacesAndSelfTo<InputInitializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}