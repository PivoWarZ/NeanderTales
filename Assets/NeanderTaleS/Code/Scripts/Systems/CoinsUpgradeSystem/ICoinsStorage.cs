using R3;

namespace NeanderTaleS.Code.Scripts.Systems.ExperienceSystem
{
    public interface ICoinsStorage
    {
        ReadOnlyReactiveProperty<int> Coins { get; }
        void AddCoins(int coins);
        void RemoveCoins(int coins);
    }
}