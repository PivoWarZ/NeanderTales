using R3;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins
{
    public class UpgradeCoinsStorage: IUpgradeCoinsStorage
    {
        private ReactiveProperty<int> _coins = new();
        
        public ReadOnlyReactiveProperty<int> Coins => _coins;
        
        void IUpgradeCoinsStorage.AddCoins(int coins)
        {
            _coins.Value += coins;
        }

        void IUpgradeCoinsStorage.RemoveCoins(int coins)
        {
            _coins.Value -= coins;
        }

        bool IUpgradeCoinsStorage.HasCoins(int coins)
        {
            return Coins.CurrentValue >= coins;
        }
    }
}