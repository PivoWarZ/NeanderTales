using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage
{
    public interface IUpgradeCoinsStorage
    {
        ReadOnlyReactiveProperty<int> Coins { get; }
        void AddCoins(int coins);
        void RemoveCoins(int coins);
        bool HasCoins(int coins);
    }
}