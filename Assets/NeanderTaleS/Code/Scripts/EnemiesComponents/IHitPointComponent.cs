using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public interface IHitPointComponent
    {
        void TakeDamage(float damage);
    }
}