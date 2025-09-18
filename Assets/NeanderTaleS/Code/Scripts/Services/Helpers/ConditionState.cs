using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Services.Helpers
{
    public class ConditionState: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;

        [Button]
        private void PrintConditionState()
        {
            var conditionComponents = _localProvider.GetInterfaces<IConditionComponent>();

            foreach (var conditionComponent in conditionComponents)
            {
                Debug.Log($"<color=green>{conditionComponent.GetType()}</color> ==> {PrintBoolean(conditionComponent.GetCompositeCondition().IsTrue)}");
                conditionComponent.GetCompositeCondition().Print();
            }
        }
        
        private string PrintBoolean(Func<bool> condition)
        {
            if (condition.Invoke())
            {
                return "<color=green>TRUE</color>";
            }

            return "<color=red>FALSE</color>";
        }
    }
}