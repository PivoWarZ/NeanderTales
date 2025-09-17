using System;
using NeanderTaleS.Code.Scripts.Condition;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public interface IConditionComponent
    {
        void AddCondition(Func<bool> condition);
        void RemoveCondition(Func<bool> condition);
        
        CompositeCondition GetCompositeCondition();
    }
}