using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates
{
    public class PlayerStatePresenter
    {
        private readonly PlayerStatsModel _model;
        private readonly PlayerStateView _view;
        private readonly CompositeDisposable _disposables = new ();

        public PlayerStatePresenter(PlayerStatsModel model, PlayerStateView view)
        {
            _model = model;
            _view = view;
        }

        public void Init()
        {
            var hpDispose = _model.CurrentHitPoints.Merge(_model.MaxHitPoints).Subscribe(SetHealthBar);
            var staminaDispose = _model.CurrentStamina.Merge(_model.MaxStamina).Subscribe(SetStaminaBar);
            var expDispose = _model.CurrentExperience.Merge(_model.RequiredExperience).Subscribe(SetExperienceBar);
            var lvlDispose = _model.Level.Subscribe(SetPlayerLevel);
            
            _disposables.Add(hpDispose);
            _disposables.Add(staminaDispose);
            _disposables.Add(expDispose);
            _disposables.Add(lvlDispose);
            
            LoadSprite();
            HideLevelUpMarker();
        }
        
        private void SetPlayerLevel(int level)
        {
            _view.Level.text = level.ToString();
        }

        private void HideLevelUpMarker()
        {
            _view.LevelUpMarker.gameObject.SetActive(false);
        }

        private void LoadSprite()
        {
            var sprite = Resources.Load<Sprite>("PlayerLogo");
            _view.Portrait.sprite = sprite;
        }

        private void SetHealthBar(float currentHitPoints)
        {
            var newValue = _model.CurrentHitPoints.CurrentValue / _model.MaxHitPoints.CurrentValue;
            
            if (Mathf.Approximately(_model.MaxHitPoints.CurrentValue, 0))
            {
                newValue = 0;
            }

            newValue = Mathf.Clamp(newValue, 0, 1);

            _view.SetHealth(newValue);
        }

        private void SetStaminaBar(float currentStaminaValue)
        {
            var newValue = _model.CurrentStamina.CurrentValue / _model.MaxStamina.CurrentValue;
            
            if (Mathf.Approximately(_model.MaxHitPoints.CurrentValue, 0))
            {
                newValue = 0;
            }

            newValue = Mathf.Clamp(newValue, 0, 1);
            
            _view.SetStamina(newValue);
        }

        private void SetExperienceBar(float _)
        {
            var newValue = _model.CurrentExperience.CurrentValue / _model.RequiredExperience.CurrentValue;
            _view.SetExperience(newValue);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}