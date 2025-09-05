using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyMoveConditionInstaller: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _target;
        [SerializeField] private EnemyMoveComponent _move;

        private void Awake()
        {
            _move.SetCondition((() => _target.CanLoockTarget));
        }
    }
}