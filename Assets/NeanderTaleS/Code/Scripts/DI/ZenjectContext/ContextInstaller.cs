using NeanderTaleS.Code.Scripts.InputSysytem;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using NeanderTaleS.Code.Scripts.RotateSystem;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI.ZenjectContext
{
    public class ContextInstaller: MonoInstaller
    {
        [SerializeField] Player _player;
        public override void InstallBindings()
        {
            IMovable movable = _player.GetComponent<IMovable>();
            IRotatable rotatable = _player.GetComponent<IRotatable>();
            
            BindRotateSystem(rotatable);
            BindInputSystem(movable);
        }

        private void BindRotateSystem(IRotatable rotatable)
        {
            if (rotatable == null)
            {
                Debug.LogWarning("IRotatable instance is null. Input system bindings may not be fully configured.");
            }
            
            Container.BindInterfacesAndSelfTo<CursorPositionListener>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<RotateController>()
                .AsSingle()
                .WithArguments(rotatable)
                .NonLazy();
        }

        private void BindInputSystem(IMovable movable)
        {
            if (movable == null)
            {
                Debug.LogWarning("IMovable instance is null. Input system bindings may not be fully configured.");
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