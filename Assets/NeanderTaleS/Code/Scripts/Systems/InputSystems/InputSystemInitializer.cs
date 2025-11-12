using NeanderTaleS.Code.Scripts.Systems.InputSystems.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems
{
    public sealed class InputSystemInitializer
    {
        private readonly IPlayerInput[] _playerInputs;
        
        public InputSystemInitializer(IPlayerInput[] playerInputs)
        {
            _playerInputs = playerInputs;
        }
        
        public void ConstructInputSystem(GameObject player)
        {
            foreach (var playerInput in _playerInputs)
            {
                playerInput.Construct(player);
            }
        }
    }
}