using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents
{
    public class PlayerProvider: MonoBehaviour
    {
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _visual;

        public Animator Animator => _animator;

        public AttackComponent AttackComponent => _attackComponent;

        public Transform Visual => _visual;

        public MoveComponent MoveComponent1 => _moveComponent;
    }
}