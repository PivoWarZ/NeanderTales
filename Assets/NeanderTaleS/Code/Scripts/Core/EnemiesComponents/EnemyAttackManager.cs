using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class EnemyAttackManager: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _target;
        [SerializeField] private AttackOnTimerComponent _timer;
        [SerializeField] private EnemyAttackComponent _attackComponent;

        private void Update()
        {
            if (!_target.Target)
            {
                return;
            }

            if (!_timer.IsAttackReady)
            {
                return;
            }

            if (_attackComponent.IsAttackReady())
            {
                _attackComponent.Attack();
            }
        }
    }
}