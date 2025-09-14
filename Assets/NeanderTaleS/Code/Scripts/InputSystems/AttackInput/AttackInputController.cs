using System;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.AttackInput
{
    public class AttackInputController: IInitializable, IDisposable
    {
        private AttackInputListener _listener;
        private IAttackable _attackable;

        public AttackInputController(AttackInputListener listener, IAttackable attackable)
        {
            _listener = listener;
            _attackable = attackable;
        }

        public void Initialize()
        {
            _listener.OnAttackInput += Attack;
        }

        private void Attack()
        {
            _attackable.Attack();
        }

        public void Dispose()
        {
            _listener.OnAttackInput -= Attack;
        }
    }
}