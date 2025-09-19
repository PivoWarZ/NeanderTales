using System;
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
            var provider = target.TryGetComponent<LocalProvider>(out var targetProvider);

            if (!provider)
            {
                return;
            }

            var debuffs = targetProvider.TryGetService<DebuffsComponent>(out var debuffsComponent);

            if (debuffs)
            {
                debuffsComponent.Pushing.Value = true;
            }

        }

        private void OnDestroy()
        {
            _leapPushing.OnPushing -= Stunned;
        }
    }
}