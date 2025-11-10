using NeanderTaleS.Code.Scripts.Systems.ExperienceSystem;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.CoinsUpgradeSystem
{
    public class CoinsStorage: ICoinsStorage
    {
        private ReactiveProperty<int> _coins = new();
        
        public ReadOnlyReactiveProperty<int> Coins => _coins;
        
        void ICoinsStorage.AddCoins(int coins)
        {
            _coins.Value += coins;
        }

        void ICoinsStorage.RemoveCoins(int coins)
        {
            _coins.Value -= coins;
        }
    }
}