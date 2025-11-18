using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats
{
    public sealed class PlayerStatsModelViewPresenterInstaller: IDisposable, IPlayerStatsModelGetter
    {
        private readonly PlayerStatsView _View;
        private PlayerStatsPresenter _presenter;
        private PlayerStatsModel _model;
        
        public  PlayerStatsModelViewPresenterInstaller(HudUI hudUI)
        {
            _View = hudUI.PlayerStatsView;
        }
        
        public PlayerStatsModel PlayerStatsModel => _model;

        public void ConstructModelViewPresenter(GameObject player)
        {
             _model = CreatePlayerModel(player);
            _presenter = new PlayerStatsPresenter(_model, _View);
            _presenter.Init();
        }

        private PlayerStatsModel CreatePlayerModel(GameObject player)
        {
            PlayerStatsModel model = new PlayerStatsModel
            {
                MaxHitPoints = player.GetComponent<IHitPointsComponent>().MaxHitPoints,
                CurrentHitPoints = player.GetComponent<IHitPointsComponent>().CurrentHitPoints,
                MaxStamina = player.GetComponent<IStaminaComponent>().MaxStamina,
                CurrentStamina = player.GetComponent<IStaminaComponent>().Stamina,
            };

            return model;
        }

        void IDisposable.Dispose()
        {
            _presenter?.Dispose();
        }
    }
}