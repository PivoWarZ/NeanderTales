using System;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Systems.EventBus;
using NeanderTaleS.Code.Scripts.Systems.EventBus.Events;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace NeanderTaleS.Code.Scripts.Systems.Factory
{
    public sealed class PlayerInstaller: IPlayerCreator
    {
        private DiContainer _context;
        private IEventBus _eventBus;
        private readonly GameObject _playerPrefab;
        private readonly string _prefabPath = "Prefabs/Player";
        private PlayerService _playerService;


        public PlayerInstaller(DiContainer context, PlayerService playerService, IEventBus eventBus)
        {
            _context = context;
            _playerService = playerService;
            _eventBus = eventBus;
            _playerPrefab = Resources.Load<GameObject>(_prefabPath);
            
            if (!_playerPrefab)
            {
                throw new ArgumentException($"Player Prefab not found at path: {_prefabPath}");
            }
        }

        void IPlayerCreator.CreatePlayer(Vector3 position)
        {
            GameObject player;
            
            if (!_playerService.GetPlayer())
            {
                 player  = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
                _context.InjectGameObject(player);
                _playerService.Construct(player);
            }
            else
            {
                player = _playerService.GetPlayer();
                player.transform.position = position;
            }
            
            _eventBus.RiseEvent(new InstantiatePlayerEvent(player, this.GetType().Name));
        }
    }
}