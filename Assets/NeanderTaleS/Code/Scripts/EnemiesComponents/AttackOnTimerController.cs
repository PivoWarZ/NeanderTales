using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackOnTimerController: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _target;
        [SerializeField] private AttackOnTimerComponent _timerComponent;
        [SerializeField] private EnemyAttackComponent _attackComponent;

        private void Awake()
        {
            _target.OnTargetChanged += StartAttackTimer;
            _attackComponent.OnAttackRequest += AttackRequest;
            _attackComponent.OnAttackEvent += ReloadAttack;
        }

        private void AttackRequest()
        {
            _timerComponent.IsAttackTime = false;
        }

        private void StartAttackTimer(GameObject target)
        {
            _timerComponent.StartAttackTimer();
        }

        private void ReloadAttack()
        {
            _timerComponent.StartAttackTimer();
        }
    }
}