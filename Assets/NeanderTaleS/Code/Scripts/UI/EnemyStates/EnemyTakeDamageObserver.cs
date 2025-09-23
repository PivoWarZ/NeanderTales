using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public class EnemyTakeDamageObserver: IDisposable
    {
        private EnemyStateAdapter _adapter;
        private List<ITakeDamageable> _enemies = new ();

        public EnemyTakeDamageObserver(EnemyStateAdapter adapter)
        {
            _adapter = adapter;
        }

        public void AddDamageable(ITakeDamageable damageable)
        {
            _enemies.Add(damageable);

            damageable.OnTakeDamageAction += ShowPopup;
        }

        private void ShowPopup(float damage, ITakeDamageable hitPointsComponent)
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