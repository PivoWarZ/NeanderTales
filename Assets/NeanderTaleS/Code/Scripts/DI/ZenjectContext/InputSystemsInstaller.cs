using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.InputSystems.AttackInput;
using NeanderTaleS.Code.Scripts.InputSystems.JumpIntput;
using NeanderTaleS.Code.Scripts.InputSystems.MoveInput;
using NeanderTaleS.Code.Scripts.InputSystems.RotateInput;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class InputSystemsInstaller: MonoInstaller
    {
        [FormerlySerializedAs("_entityBootsTrap")] [FormerlySerializedAs("_playerBootsTrap")] [SerializeField] Player _player;
        public override void InstallBindings()
        {
            IMovable movable = _player.GetComponent<IMovable>();
            ICursorFollower cursorFollower = _player.GetComponent<ICursorFollower>();
            IJumping jumping = _player.GetComponent<IJumping>();
            IAttackable attackable = _player.GetComponent<IAttackable>();
            
            BindAttackInput(attackable);
            BindJumpInput(jumping);
            BindRotateInput(cursorFollower);
            BindMoveInput(movable);
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

        private void BindRotateInput(ICursorFollower cursorFollower)
        {
            if (cursorFollower == null)
            {
                Debug.LogWarning("<color=yellow> IRotatable instance is null. Input system bindings may not be fully configured. </color>");
            }
            
            Container.BindInterfacesAndSelfTo<CursorPositionListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<RotateController>()
                .AsSingle()
                .WithArguments(cursorFollower)
                .NonLazy();
        }

        private void BindMoveInput(IMovable movable)
        {
            if (movable == null)
            {
                Debug.LogWarning("<color=yellow> IMovable instance is null. Input system bindings may not be fully configured. </color>");
            }

            Container.BindInterfacesAndSelfTo<PlayerInputListener>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerInputController>()
                .AsSingle()
                .WithArguments(movable)
                .NonLazy();
        }
    }
}