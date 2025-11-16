using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Repository
{
    public sealed class GameRepository: IGameRepository, IInitializable
    {
        private  Dictionary<string, string> _gameData;
        private readonly IGameStateSaver _gameStateSaver;

        public GameRepository(IGameStateSaver gameStateSaver)
        { 
            _gameStateSaver = gameStateSaver;
        }

        void IInitializable.Initialize()
        {
            _gameData = new Dictionary<string, string>();
        }

        void IGameRepository.SaveData<T>(T data)
        {
            DataVerification();

            var key = typeof(T).Name;
            _gameData[key] = JsonConvert.SerializeObject(data);
        }

        bool IGameRepository.TryGetData<T>(out T data)
        {
            DataVerification();

            var key = typeof(T).Name;

            if (_gameData.TryGetValue(key, out string value))
            {
                data = JsonConvert.DeserializeObject<T>(value);
                return true;
            }
     
            data = default;
            return false;
            
        }

        void IGameRepository.SaveState()
        {
            _gameStateSaver.SaveState(_gameData);
        }

        void IGameRepository.LoadState()
        {
            _gameData = _gameStateSaver.LoadState();
        }

        private void DataVerification()
        {
            if (_gameData == null)
            {
                Debug.Log("<color=red>GameRepository DATA NULL</color>");
            }
        }
    }
}