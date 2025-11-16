using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public sealed class PlayerService
    {
        private GameObject _player;

        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }

        public void Construct(GameObject player)
        {
            _player = player;
        }
    }
}