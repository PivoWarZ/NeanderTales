using NeanderTaleS.Code.Scripts.Systems.EventBus;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Core.Services
{
    public class PlayerService: IInitializable
    {
        private GameObject _player;
        private IEventBus _eventBus;

        public PlayerService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public GameObject GetPlayer()
        {
            return _player.gameObject;
        }

        void IInitializable.Initialize()
        {
            //_eventBus.Subscribe<CreatedPlayerEvent>(SetPlayer);
        }

        //private void SetPlayer(CreatedPlayerEvent @event)
       // {
        //    throw new System.NotImplementedException();
        //}
    }
}