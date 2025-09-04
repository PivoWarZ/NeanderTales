using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyProvider: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _targetComponent;
        [SerializeField] private Animator _animator;

        public EnemyTargetComponent TargetComponent => _targetComponent;

        public Animator Animator => _animator;
    }
}