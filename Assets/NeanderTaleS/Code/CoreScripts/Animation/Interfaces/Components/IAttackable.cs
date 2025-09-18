using System;

namespace NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components
{
    public interface IAttackable
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
        void Attack();
    }
}