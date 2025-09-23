using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components
{
    public class AdditionalDamageComponent: MonoBehaviour, IAdditionalDamage
    {
        public float AdditionalPercentDamage { get; set; }

        [SerializeField] DealDamageComponent _dealDamageComponent;

        public void Init()
        {
            _dealDamageComponent.OnDealDamageRequest += AddDamage;
        }

        public void AddDamage(ref float damage)
        {
            damage *= 1 + AdditionalPercentDamage / 100;
        }
    }
}