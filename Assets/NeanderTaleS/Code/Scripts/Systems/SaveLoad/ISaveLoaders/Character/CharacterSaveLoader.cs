using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context;
using NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.ISaveLoaders.Character
{
    public class CharacterSaveLoader: ISaveLoader
    {
        void ISaveLoader.LoadGame(IContext context, IGameRepository gameRepository)
        {
            Debug.Log($"<color=yellow>>LoadGame : {context.GetType().Name}</color>");
            var service = context.GetService<PlayerService>();
            var player = service.GetPlayer();
            var data = new CharacterData();

            bool isDataFound = gameRepository.TryGetData<CharacterStorage>(out var characterStorage);

            if (isDataFound)
            {
                data.PositionX = characterStorage.CharacterData.PositionX;
                data.PositionY = characterStorage.CharacterData.PositionY;
                data.PositionZ = characterStorage.CharacterData.PositionZ;
            }
            else
            {
                Debug.Log($"<color=red>Load Game Error: {GetType().Name} DATA not found</color>");
                return;
            }
            
            player.transform.position = new Vector3(data.PositionX, data.PositionY, data.PositionZ);
            
            Debug.Log($"<color=yellow>Load Game Data Position {data.PositionX} / {data.PositionY} / {data.PositionZ}</color>");
        }

        void ISaveLoader.SaveGame(IContext context, IGameRepository gameRepository)
        {
            var service = context.GetService<PlayerService>();
            var player = service.GetPlayer();
            
            CharacterData data = new CharacterData
            {
                PositionX = player.transform.position.x,
                PositionY = player.transform.position.y,
                PositionZ = player.transform.position.z,
            };

            CharacterStorage storage = new CharacterStorage(data);
            
            gameRepository.SaveData(storage);
            
            Debug.Log($"<color=green>Saved Game Data Position {data.PositionX} / {data.PositionY} / {data.PositionZ}</color>");
        }
    }
}