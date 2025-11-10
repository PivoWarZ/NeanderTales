using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates
{
    public sealed class PlayerStatsInstaller: IDisposable, IPlayerStatsModelGetter
    {
        private LocalProvider _provider;
        private PlayerStatsModel _model;
        private PlayerStatePresenter _presenter;
        private readonly PlayerStateView _View;
        readonly IExperienceGetter _experienceGetter;
        
        public  PlayerStatsInstaller(HudUI hudUI, IExperienceGetter experienceGetter)
        {
            _View = hudUI.PlayerStateView;
            _experienceGetter = experienceGetter;
        }

        public PlayerStatsModel PlayerStatsModel => _model;


        public void Initialize(GameObject player)
        {
            var localProvider = player.GetComponent<LocalProvider>();

            if (!localProvider)
            {
                throw new Exception($"{GetType()} Player not found");
            }
            
            _provider = localProvider;
            
            Init();
        }

        private void Init()
        {
            _model = CreatePlayerModel();
            _presenter = CreatePlayerStatePresenter();

            _presenter.Init();
        }

        private PlayerStatsModel CreatePlayerModel()
        {
            PlayerStatsModel model = new PlayerStatsModel
            {
                MaxHitPoints = _provider.GetInterface<IHitPointsComponent>().MaxHitPoints,
                CurrentHitPoints = _provider.GetInterface<IHitPointsComponent>().CurrentHitPoints,
                MaxStamina = _provider.GetInterface<IStaminaComponent>().MaxStamina,
                CurrentStamina = _provider.GetInterface<IStaminaComponent>().Stamina,
                RequiredExperience = _experienceGetter.RequiredExperience,
                CurrentExperience = _experienceGetter.CurrentExperience,
                Level = _provider.GetComponent<Player>().Level
            };

            return model;
        }

        private PlayerStatePresenter CreatePlayerStatePresenter()
        {
            PlayerStatePresenter presenter = new PlayerStatePresenter(PlayerStatsModel, _View);
            
            return presenter;
        }

        void IDisposable.Dispose()
        {
            _presenter.Dispose();
        }
    }
}