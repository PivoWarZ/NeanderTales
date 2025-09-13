using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyProvider: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _targetComponent;
        [SerializeField] private EnemyMoveComponent _moveComponent;
        [SerializeField] private EnemyAttackComponent _attackComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private DebuffsComponent _debuffComponent;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventDispatcher _eventDispatcher;

        public EnemyTargetComponent TargetComponent => _targetComponent;

        public Animator Animator => _animator;

        public EnemyMoveComponent MoveComponent => _moveComponent;

        public EnemyAttackComponent AttackComponent => _attackComponent;

        public AnimationEventDispatcher AnimationEvent => _eventDispatcher;

        public JumpComponent JumpComponent => _jumpComponent;

        public DebuffsComponent DebuffComponent => _debuffComponent;

        public Rigidbody Rigidbody => _rigidbody;
    }
}