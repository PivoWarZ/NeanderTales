using System;
using NeanderTaleS.Code.CoreScripts.Condition;

namespace NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components
{
    public interface IConditionComponent
    {
        void AddCondition(Func<bool> condition);
        void RemoveCondition(Func<bool> condition);
        
        CompositeCondition GetCompositeCondition();
    }
}