using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class DealDamageComponent:MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _canDamage;
        
        public void DealDamage(IHitPointComponent hitPoint)
        {
            if (_canDamage)
            {
                return;
            }
            
            hitPoint.TakeDamage(_damage);
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}