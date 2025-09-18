using System;
using NeanderTaleS.Code.Scripts.Condition;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.Components
{
    public interface IConditionComponent
    {
        void AddCondition(Func<bool> condition);
        void RemoveCondition(Func<bool> condition);
        
        CompositeCondition GetCompositeCondition();
    }
}