using NeanderTaleS.Code.Scripts.Core.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemySkills
{
    public class LeapPushStunned: MonoBehaviour
    {
        [SerializeField] private LeapPushing _leapPushing;

        private void Awake()
        {
            _leapPushing.OnPushing += Stunned;
        }

        private void Stunned(GameObject target)
        {
            var targetProvider = target.GetComponent<LocalProvider>();
            var debuffs = targetProvider.GetService<DebuffsComponent>();
            
            debuffs.Pushing.Value = true;
        }
    }
}