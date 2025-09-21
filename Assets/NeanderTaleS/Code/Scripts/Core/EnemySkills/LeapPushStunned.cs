using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NeanderTaleS.Code.Scripts.Core.EnemySkills
{
    public class LeapPushStunned: MonoBehaviour
    {
        [SerializeField] private LeapPushing _leapPushing;
        [SerializeField] private float _chance;

        private void Awake()
        {
            _leapPushing.OnPushing += Stunned;
        }

        private void Stunned(GameObject target)
        {
            if (!IsStunLeaping())
            {
                return;
            }

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

        private bool IsStunLeaping()
        {
            var value = Random.Range(0, 100);
            
            return value <= _chance;
        }

        public void SetStunnedChance(float chance)
        {
            _chance = chance;
        }

        private void OnDestroy()
        {
            _leapPushing.OnPushing -= Stunned;
        }
    }
}