using NeanderTaleS.Code.Scripts.Animation;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents
{
    public class PlayerProvider: MonoBehaviour
    {
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;

        public AttackComponent AttackComponent => _attackComponent;
    }
}