using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.LevelCounter
{
    public interface ILevelUpCounter
    {
        ReadOnlyReactiveProperty<int> Level { get; }
        void SetLevel(int level);
        void Dispose();
    }
}