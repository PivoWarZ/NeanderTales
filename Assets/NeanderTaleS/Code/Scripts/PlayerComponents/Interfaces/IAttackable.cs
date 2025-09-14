using System;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces
{
    public interface IAttackable
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
        void Attack();
    }
}