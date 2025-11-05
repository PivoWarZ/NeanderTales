using System;
using NeanderTaleS.Code.Scripts.Systems.InputSystems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NeanderTaleS.Code.Scripts.Systems.Factory
{
    public sealed class PlayerInstaller: IPlayerCreator, IInitializePlayer
    {
        private readonly IInitializedAsPlayer[] _initializedAsPlayers;
        private readonly GameObject _playerPrefab;
        private readonly string _prefabPath = "Prefabs/Player";


        public PlayerInstaller(IInitializedAsPlayer[] initializedAsPlayers)
        {
            _initializedAsPlayers = initializedAsPlayers;
            _playerPrefab = Resources.Load<GameObject>(_prefabPath);
            
            if (!_playerPrefab)
            {
                throw new ArgumentException($"Player Prefab not found at path: {_prefabPath}");
            }
        }

        void IPlayerCreator.CreatePlayer(Vector3 position)
        {
            var player  = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
            ((IInitializePlayer)this).InitializePlayer(player);
        }

        void IInitializePlayer.InitializePlayer(GameObject player)
        {
            foreach (var initializedAsPlayer in _initializedAsPlayers)
            {
                initializedAsPlayer.Initialize(player);
            }
        }
    }
}