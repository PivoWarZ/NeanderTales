using System;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad;
using R3;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Observers
{
    public class CharacterDyingObserver_LoadGame: IInitializable, IDisposable
    {
        private GameCycleManager _gameCycle;
        private SaveLoadManager _saveLoad;
        private IHitPointsComponent _hitpoints;
        private GameObject _player;
        private IDisposable _disposable;

        public CharacterDyingObserver_LoadGame(PlayerService playerService, GameCycleManager gameCycle, SaveLoadManager saveLoad)
        {
            _gameCycle = gameCycle;
            _saveLoad = saveLoad;
            _hitpoints = playerService.GetPlayer().GetComponent<IHitPointsComponent>();
            _player = playerService.GetPlayer();
        }

        void IInitializable.Initialize()
        {
           _disposable = _hitpoints.CurrentHitPoints.Where(hp => hp <= 0).Subscribe(Dying);
        }

        private void Dying(float _)
        {
            if (!_player.activeSelf)
            {
                return;
            }

            _gameCycle.PauseGame();
            _saveLoad.LoadGame();
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}