using System;
using NeanderTaleS.Code.Scripts.Systems.Experience.LevelCounter;
using R3;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.LevelCounter
{
    public sealed class LevelUpCounterAdapter: IDisposable
    {
        private ILevelUpCounter _level;
        private LevelUPView _upView;
        private IDisposable _disposable;

        public LevelUpCounterAdapter(ILevelUpCounter level, HudUI hudUI)
        {
            _level = level;
            _upView = hudUI.LevelUpView;
            _disposable = _level.Level.Subscribe(IncreamentLevelCount);
        }

        private void IncreamentLevelCount(int level)
        {
            _upView.SetLevel(level);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}