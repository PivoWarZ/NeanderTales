using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.Experience.UpgradeCoinsStorage;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.SaveLoaders.UpgradeCoins
{
    public sealed class CoinsSaveLoad: ISaveLoader
    {
        void ISaveLoader.LoadGame(IContext context, IGameRepository gameRepository)
        {
            bool isDataFound = gameRepository.TryGetData<CoinsDataStorage>(out var dataStorage);

            if (!isDataFound)
            {
                Debug.Log($"<color=red>Load Game Error: {GetType().Name} DATA not found</color>");
                return;
            }
            
            IUpgradeCoinsStorage coinsStorage = context.GetService<IUpgradeCoinsStorage>();
            var currentCoins = coinsStorage.Coins.CurrentValue;
            var uploadingCoins = dataStorage.Data.Coins;
            coinsStorage.AddCoins(uploadingCoins - currentCoins);
            
            Debug.Log($"<color=yellow>Coins Loading: {coinsStorage.Coins.CurrentValue}</color>");
        }

        void ISaveLoader.SaveGame(IContext context, IGameRepository gameRepository)
        {
            IUpgradeCoinsStorage coinsStorage = context.GetService<IUpgradeCoinsStorage>();
            int coins = coinsStorage.Coins.CurrentValue;
            
            CoinsData data = new CoinsData
            {
                Coins = coins
            };

            CoinsDataStorage storage = new CoinsDataStorage(data);
            gameRepository.SaveData(storage);
            
            Debug.Log($"<color=green>Coins saved: {coins}</color>");
        }
    }
}