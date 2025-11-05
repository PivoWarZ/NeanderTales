using System;

namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IAttackEvents
    {
        event Action OnAttackRequest;
        event Action OnAttackAction;
        event Action OnAttackEvent;
    }
}