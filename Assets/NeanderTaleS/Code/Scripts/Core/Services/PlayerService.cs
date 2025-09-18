using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public class PlayerService
    {
        private Player _player;

        public PlayerService(Player player)
        {
            _player = player;
        }

        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }
    }
}