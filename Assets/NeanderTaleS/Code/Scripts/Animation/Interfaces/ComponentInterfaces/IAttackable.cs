using System;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces
{
    public interface IAttackable
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
        void Attack();
    }
}