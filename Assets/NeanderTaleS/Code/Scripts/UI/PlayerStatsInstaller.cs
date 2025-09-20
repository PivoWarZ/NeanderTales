using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class PlayerStatsInstaller: IDisposable
    {
        private LocalProvider _provider;
        private PlayerStatsModel _model;
        private PlayerStatePresenter _presenter;
        private PlayerStateView _View;
        IExperienceStorage _experienceStorage;
        
        public  PlayerStatsInstaller(PlayerService service, HudUI hudUI, IExperienceStorage experienceStorage)
        {
            var player = service.GetPlayer();
            _provider = player.GetComponent<LocalProvider>();
            _View = hudUI.PlayerStateView;
            _experienceStorage = experienceStorage;
            
            Init();
        }

        public void Init()
        {
            _model = CreatePlayerModel();
            _presenter = CreatePlayerStatePresenter();

            _presenter.Init();
        }

        private PlayerStatsModel CreatePlayerModel()
        {
            PlayerStatsModel model = new PlayerStatsModel
            {
                MaxHitPoints = _provider.GetInterface<ITakeDamageble>().MaxHitPoints,
                CurrentHitPoints = _provider.GetInterface<ITakeDamageble>().CurrentHitPoints,
                MaxStamina = _provider.GetInterface<IStamina>().MaxStamina,
                CurrentStamina = _provider.GetInterface<IStamina>().Stamina,
                RequiredExperience = _experienceStorage.RequiredExperience,
                CurrentExperience = _experienceStorage.CurrentExperience,
                Level = _experienceStorage.Level
            };
            
            return model;
        }

        private PlayerStatePresenter CreatePlayerStatePresenter()
        {
            PlayerStatePresenter presenter = new PlayerStatePresenter(_model, _View);
            
            return presenter;
        }

        void IDisposable.Dispose()
        {
            _presenter.Dispose();
        }
    }
}