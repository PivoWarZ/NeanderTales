using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems
{
    public sealed class InputInitializer: IInitializedAsPlayer
    {
        private readonly IPlayerInput[] _playerInputs;

        public InputInitializer(IPlayerInput[] playerInputs)
        {
            _playerInputs = playerInputs;
        }

        public void Initialize(GameObject player)
        {
            foreach (var playerInput in _playerInputs)
            {
                playerInput.Init(player);
            }
        }
    }
}