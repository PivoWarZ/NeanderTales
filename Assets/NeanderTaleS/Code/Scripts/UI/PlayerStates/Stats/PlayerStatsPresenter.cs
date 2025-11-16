using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats
{
    public sealed class PlayerStatsPresenter
    {
        private readonly PlayerStatsModel _model;
        private readonly PlayerStatsView _view;
        private readonly CompositeDisposable _disposables = new ();

        public PlayerStatsPresenter(PlayerStatsModel model, PlayerStatsView view)
        {
            _model = model;
            _view = view;
        }

        public void Init()
        {
            var hpDispose = _model.CurrentHitPoints.Merge(_model.MaxHitPoints).Subscribe(SetHealthBar);
            var staminaDispose = _model.CurrentStamina.Merge(_model.MaxStamina).Subscribe(SetStaminaBar);
            
            _disposables.Add(hpDispose);
            _disposables.Add(staminaDispose);
        }
        
        private void SetHealthBar(float _)
        {
            var currentValue = _model.CurrentHitPoints.CurrentValue;
            var maxValue = _model.MaxHitPoints.CurrentValue;
            
            var newValue = maxValue > 0 ? currentValue / maxValue : 0;
            
            if (Mathf.Approximately(_model.MaxHitPoints.CurrentValue, 0))
            {
                newValue = 0;
            }

            newValue = Mathf.Clamp(newValue, 0, 1);

            _view.SetHealth(newValue);
        }

        private void SetStaminaBar(float _)
        {
            var currentValue = _model.CurrentStamina.CurrentValue;
            var maxValue = _model.MaxStamina.CurrentValue;
            
            var newValue = maxValue > 0 ? currentValue / maxValue : 0;
            
            if (Mathf.Approximately(_model.MaxHitPoints.CurrentValue, 0))
            {
                newValue = 0;
            }

            newValue = Mathf.Clamp(newValue, 0, 1);
            
            _view.SetStamina(newValue);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}