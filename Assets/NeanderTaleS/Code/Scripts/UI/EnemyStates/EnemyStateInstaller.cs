using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public class EnemyStateInstaller
    {
        GameObject _dino;
        HudUI _hud;
        private EnemyStateAdapter _adapter;
        private EnemyStateView _view;

        private void Awake()
        {
            ITakeDamageable takeDamageable = _dino.GetComponent<ITakeDamageable>();
            takeDamageable.OnTakeDamageAction += ShowPopup;

            _view = _hud.EnemyStatesView;
            _view.gameObject.SetActive(false);
            
            _adapter = new EnemyStateAdapter(_view);
        }

        private void ShowPopup(float damage, ITakeDamageable hitPointsComponent)
        {
            _adapter.Init(damage, hitPointsComponent);
        }

        private void OnDestroy()
        {
            _adapter.Dispose();
        }
    }
}