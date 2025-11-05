using NeanderTaleS.Code.Scripts.Interfaces.Components;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public interface IDealDamage
    {
        void DealDamage(IDamageable damageable, float damage);
    }
}