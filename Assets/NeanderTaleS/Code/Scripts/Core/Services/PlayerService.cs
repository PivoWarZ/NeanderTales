using NeanderTaleS.Code.Scripts.Systems.InputSystems;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public class PlayerService: IInitializedAsPlayer
    {
        private GameObject _player;

        public void Initialize(GameObject player)
        {
            _player = player;
            Debug.Log(player.name + " has been initialized.");
        }
        
        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }
    }
}