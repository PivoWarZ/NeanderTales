using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackDistanceController: MonoBehaviour
    {
        [SerializeField] private AttackDistanceComponent _attackDistanceComponent;
        [SerializeField] private EnemyAttackComponent _enemyAttackComponent;
        [SerializeField] private EnemyMoveComponent _enemyMoveComponent;

        private void Awake()
        {
            _enemyMoveComponent.AddCondition(() => !IsAttackDistance());
            _enemyAttackComponent.AddCondition(IsAttackDistance);
        }

        private bool IsAttackDistance()
        {
            return _attackDistanceComponent.IsAttackDistance;
        }
    }
}