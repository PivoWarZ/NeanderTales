using System;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IAttackable
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
        void Attack();
    }
}