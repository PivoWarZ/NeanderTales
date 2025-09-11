using System;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemySkills
{
    public class LeapSkillInstallConditions: MonoBehaviour
    {
        [SerializeField] private LeapSkill _leapSkill;
        [SerializeField] private EnemyMoveComponent _moveComponent;
        [SerializeField] private EnemyRotateComponent _rotateComponent;
        [SerializeField] private AttackComponent _attackComponent;

        private void OnEnable()
        {
            _moveComponent.SetCondition(() => !_leapSkill.IsLeapAttack);
            _rotateComponent.SetCondition(() => !_leapSkill.IsLeapAttack);
            _attackComponent.SetCondition(() => !_leapSkill.IsLeapAttack);
        }

        private void OnDisable()
        {
            _moveComponent.RemoveCondition(() => !_leapSkill.IsLeapAttack);
            _rotateComponent.RemoveCondition(() => !_leapSkill.IsLeapAttack);
            _attackComponent.RemoveCondition(() => !_leapSkill.IsLeapAttack);
        }
    }
}