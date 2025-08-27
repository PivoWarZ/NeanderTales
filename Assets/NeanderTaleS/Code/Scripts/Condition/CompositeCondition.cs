using System;
using System.Collections.Generic;

namespace NeanderTaleS.Code.Scripts.Condition
{
    public class CompositeCondition
    {
        public List<Func<bool>> _compositeConditions = new();

        public void AddCondition(Func<bool> condition)
        {
            _compositeConditions.Add(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _compositeConditions.Remove(condition);
        }

        public bool IsTrue()
        {
            foreach (var compositeCondition in _compositeConditions)
            {
                if (!compositeCondition())
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}