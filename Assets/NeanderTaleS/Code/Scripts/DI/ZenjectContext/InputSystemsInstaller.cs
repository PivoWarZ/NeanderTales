using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.InputSystems.AttackInput;
using NeanderTaleS.Code.Scripts.InputSystems.JumpIntput;
using NeanderTaleS.Code.Scripts.InputSystems.MoveInput;
using NeanderTaleS.Code.Scripts.InputSystems.RotateInput;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class InputSystemsInstaller: MonoInstaller
    {
        [SerializeField] Player _player;
        public override void InstallBindings()
        {
            IMovable movable = _player.GetComponent<IMovable>();
            IRotatable rotatable = _player.GetComponent<IRotatable>();
            IJumping jumping = _player.GetComponent<IJumping>();
            IAnimationController animationController = _player.GetComponent<IAnimationController>();
            IAttackable attackable = _player.GetComponent<IAttackable>();
            
            BindAttackInput(attackable);
            BindJumpInput(jumping);
            BindRotateInput(rotatable);
            BindMoveInput(movable, animationController);
        }

        private void BindAttackInput(IAttackable attackable)
        {
            if (attackable == null)
            {
                Debug.LogWarning("<color=yellow> IAnimationController instance is null. Input system bindings may not be fully configured. </color>");
            }
            
            Container.BindInterfacesAndSelfTo<AttackInputListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<AttackInputController>()
                .AsSingle()
                .WithArguments(attackable)
                .NonLazy();
        }

        private void BindJumpInput(IJumping jumping)
        {
            if (jumping == null)
            {
                Debug.LogWarning("<color=yellow> IJumping instance is null. Input system bindings may not be fully configured. </color>");
            }
            
            Container.BindInterfacesAndSelfTo<JumpInputListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<JumpInputController>()
                .AsSingle()
                .WithArguments(jumping)
                .NonLazy();
        }

        private void BindRotateInput(IRotatable rotatable)
        {
            if (rotatable == null)
            {
                Debug.LogWarning("<color=yellow> IRotatable instance is null. Input system bindings may not be fully configured. </color>");
            }
            
            Container.BindInterfacesAndSelfTo<CursorPositionListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<RotateController>()
                .AsSingle()
                .WithArguments(rotatable)
                .NonLazy();
        }

        private void BindMoveInput(IMovable movable, IAnimationController animationController)
        {
            if (movable == null)
            {
                Debug.LogWarning("<color=yellow> IMovable instance is null. Input system bindings may not be fully configured. </color>");
            }
            
            if (animationController == null)
            {
                Debug.LogWarning("<color=yellow> IAnimationController instance is null. Input system bindings may not be fully configured. </color>");
            }

            Container.BindInterfacesAndSelfTo<PlayerInputListener>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerInputController>()
                .AsSingle()
                .WithArguments(movable, animationController)
                .NonLazy();
        }
    }
}