using System;
using NeanderTaleS.Code.Scripts.Core.Condition;

namespace NeanderTaleS.Code.Scripts.Core.Interfaces.Components
{
    public interface IConditionComponent
    {
        void AddCondition(Func<bool> condition);
        void RemoveCondition(Func<bool> condition);
        
        CompositeCondition GetCompositeCondition();
    }
}