using System;
using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class WeaponInitializer: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;

        private void Start()
        {
            bool hasWeapon = _localProvider.TryGetService<Weapon>(out var weapon);

            if (hasWeapon)
            {
                DealDamageComponent dealDamage = _localProvider.GetService<DealDamageComponent>();
                weapon.Init(dealDamage);
            }
        }
    }
}