using System;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class PlayerStatePresenter
    {
        private PlayerStatsModel _model;
        private PlayerStateView _view;
        private CompositeDisposable _disposables = new ();

        public PlayerStatePresenter(PlayerStatsModel model, PlayerStateView view)
        {
            _model = model;
            _view = view;
        }

        public void Init()
        {
            var hpDispose = _model.CurrentHitPoints.Subscribe(SetHealthBar);
            var staminaDispose = _model.CurrentStamina.Subscribe(SetStaminaBar);
            var expDispose = _model.CurrentExperience.Subscribe(SetExperienceBar);
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
            var newValue = currentHitPoints / _model.MaxHitPoints.CurrentValue;
            _view.SetHealth(newValue);
        }

        private void SetStaminaBar(float currentStaminaValue)
        {
            var newValue = currentStaminaValue / _model.MaxStamina.CurrentValue;
            _view.SetStamina(newValue);
        }

        private void SetExperienceBar(float currentExperience)
        {
            var newValue = currentExperience / _model.RequiredExperience.CurrentValue;
            _view.SetExperience(newValue);
        }
        
        public void Dispose()
        {
            Debug.Log("PlayerStatePresenter.Dispose");
            _disposables.Dispose();
        }
    }
}