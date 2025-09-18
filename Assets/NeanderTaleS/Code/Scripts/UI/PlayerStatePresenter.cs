using System;
using R3;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class PlayerStatePresenter: IDisposable
    {
        private PlayerStatsModel _model;
        private PlayerStateView _view;
        private CompositeDisposable _disposables = new ();

        public PlayerStatePresenter(PlayerStatsModel model, PlayerStateView view)
        {
            _model = model;
            _view = view;

            var hpDispose = _model.HitPoints.Subscribe(SetHealthBar);
            var staminaDispose = _model.Stamina.Subscribe(SetStaminaBar);
            var expDispose = _model.Experience.Subscribe(SetExperienceBar);
            
            _disposables.Add(hpDispose);
            _disposables.Add(staminaDispose);
            _disposables.Add(expDispose);
        }

        private void SetHealthBar(float currentHitPoints)
        {
            _view.Health.value = currentHitPoints / _model.MaxStaminaValue.CurrentValue;
        }

        private void SetStaminaBar(float currentStaminaValue)
        {
            _view.Stamina.value = currentStaminaValue / _model.MaxStaminaValue.CurrentValue;
        }

        private void SetExperienceBar(float currentExperience)
        {
            _view.Experience.value = currentExperience / _model.RequiredExperience.CurrentValue;
        }
        
        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}