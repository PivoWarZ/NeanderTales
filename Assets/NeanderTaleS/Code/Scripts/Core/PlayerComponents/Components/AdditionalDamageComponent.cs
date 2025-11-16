using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components
{
    public sealed class AdditionalDamageComponent: MonoBehaviour, IAdditionalDamage
    {
        public float AdditionalPercentDamage { get; set; }

        [SerializeField] DealDamageComponent dealDamageComponent;

        public void Init()
        {
            dealDamageComponent.OnDealDamageRequest += AddDamage;
        }

        public void AddDamage(ref float damage)
        {
            damage *= 1 + AdditionalPercentDamage / 100;
        }
    }
}