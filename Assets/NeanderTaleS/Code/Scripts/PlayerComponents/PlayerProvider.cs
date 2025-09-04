using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents
{
    public class PlayerProvider: MonoBehaviour
    {
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private RotateComponent_LookAtCursor _rotateComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _visual;

        public Animator Animator => _animator;

        public AttackComponent AttackComponent => _attackComponent;

        public Transform Visual => _visual;

        public MoveComponent MoveComponent => _moveComponent;

        public RotateComponent_LookAtCursor RotateComponent => _rotateComponent;

        public JumpComponent JumpComponent => _jumpComponent;
    }
}