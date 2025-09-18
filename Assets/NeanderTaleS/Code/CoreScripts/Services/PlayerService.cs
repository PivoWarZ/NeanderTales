using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Services
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
            return _player;
        }
    }
}