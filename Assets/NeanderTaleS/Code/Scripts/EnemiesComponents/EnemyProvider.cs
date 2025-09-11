using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyProvider: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _targetComponent;
        [SerializeField] private EnemyMoveComponent _moveComponent;
        [SerializeField] private EnemyAttackComponent _attackComponent;
        [SerializeField] private Animator _animator;

        public EnemyTargetComponent TargetComponent => _targetComponent;

        public Animator Animator => _animator;

        public EnemyMoveComponent MoveComponent => _moveComponent;

        public EnemyAttackComponent AttackComponent => _attackComponent;
    }
}