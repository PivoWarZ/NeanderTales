using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class DebuffsComponent: MonoBehaviour
    {
        [SerializeField] public SerializableReactiveProperty<bool> Pushing = new(false);
        [SerializeField] private ConditionInstaller _conditionInstaller;

        public void Init()
        {
            SetStunCondition();
        }

        private void SetStunCondition()
        {
            _conditionInstaller.AddCondition<IMovable>(IsStunOver);
            _conditionInstaller.AddCondition<IRotatable>(IsStunOver);
            _conditionInstaller.AddCondition<IAttackEvents>(IsStunOver);
            _conditionInstaller.AddCondition<IJumping>(IsStunOver);
        }

        public bool IsStunOver()
        {
            return !Pushing.Value;
        }
    }
}