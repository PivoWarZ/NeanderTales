using System;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter
{
    public sealed class LevelUpCounter: ILevelUpCounter, IDisposable
    {
        private ReactiveProperty<int> _level = new(0);
        public ReadOnlyReactiveProperty<int> Level => _level;

        void ILevelUpCounter.LevelUp()
        {
            _level.Value++;
        }

        public int GetLevel()
        {
            return Level.CurrentValue;
        }

        public void Dispose()
        {
            _level.Dispose();
        }
    }
}