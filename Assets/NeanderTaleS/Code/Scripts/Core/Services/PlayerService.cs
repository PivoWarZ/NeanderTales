using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public class PlayerService: IInitializedAsPlayer
    {
        private GameObject _player;

        void IInitializedAsPlayer.Initialize(GameObject player)
        {
            _player = player;
        }
        
        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }
    }
}