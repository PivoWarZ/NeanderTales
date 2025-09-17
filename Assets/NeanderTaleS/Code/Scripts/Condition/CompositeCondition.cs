using System;
using System.Collections.Generic;
using UnityEngine;

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

        public void Print()
        {
            foreach (var compositeCondition in _compositeConditions)
            {
                Debug.Log($"<color=yellow>{compositeCondition.Method} => {compositeCondition.Invoke()}</color>");
            }
        }
    }
}