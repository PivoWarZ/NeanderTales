using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Interfaces.Components;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public sealed class EnemyTakeDamageHandler_ShowPopup: IDisposable
    {
        private readonly EnemyStateAdapter _adapter;
        private List<ITakeDamageEvents> _enemies = new ();

        public EnemyTakeDamageHandler_ShowPopup(EnemyStateAdapter adapter)
        {
            _adapter = adapter;
        }

        public void AddDamageable(ITakeDamageEvents damageEvents)
        {
            _enemies.Add(damageEvents);

            damageEvents.OnTakeDamageAction += ShowPopup;
        }

        private void ShowPopup(float damage, IHitPointsComponent hitPointsComponent)
        {
            _adapter.Construct(damage, hitPointsComponent);
        }

        public void Dispose()
        {
            if (_enemies.Count > 0)
            {
                for (var index = 0; index < _enemies.Count; index++)
                {
                    var takeDamageable = _enemies[index];
                    takeDamageable.OnTakeDamageAction -= ShowPopup;
                }
            }
            
            _enemies.Clear();
            _enemies = null;
        }
    }
}