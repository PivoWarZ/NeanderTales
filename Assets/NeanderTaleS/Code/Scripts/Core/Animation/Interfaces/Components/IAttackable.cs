using System;

namespace NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Components
{
    public interface IAttackable
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
        void Attack();
    }
}