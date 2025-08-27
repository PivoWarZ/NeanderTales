using System;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using Zenject;

namespace NeanderTaleS.Code.JumpSystem
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