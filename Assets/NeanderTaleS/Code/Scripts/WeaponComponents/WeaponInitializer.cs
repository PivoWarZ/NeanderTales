using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;


namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class WeaponInitializer
    {
        public void Init(LocalProvider localProvider)
        {
            bool hasWeapon = localProvider.TryGetService<Weapon>(out var weapon);

            if (hasWeapon)
            {
                DealDamageComponent dealDamage = localProvider.GetService<DealDamageComponent>();
                IAttackable attackComponent = localProvider.GetInterface<IAttackable>();
                weapon.Init(dealDamage, attackComponent);
            }
        }
    }
}