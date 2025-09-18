using System;
using NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Components;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Core.InputSystems.JumpIntput
{
    public class JumpInputController: IInitializable, IDisposable
    {
        private JumpInputListener _listener;
        private IJumping _jumping;

        public JumpInputController(JumpInputListener listener, IJumping jumping)
        {
            _listener = listener;
            _jumping = jumping;
        }

        void IInitializable.Initialize()
        {
            _listener.OnJump += Jump;
        }

        private void Jump()
        {
            _jumping.Jump();
        }

        void IDisposable.Dispose()
        {
            _listener.OnJump -= Jump;
        }
    }
}