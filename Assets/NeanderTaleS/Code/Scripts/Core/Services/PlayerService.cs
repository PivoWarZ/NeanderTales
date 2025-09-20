using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public class PlayerService
    {
        private GameObject _player;

        public PlayerService(GameObject player)
        {
            _player = player;
        }

        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }
    }
}