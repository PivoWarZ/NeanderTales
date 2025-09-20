using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class HudUI: MonoBehaviour
    {
        [SerializeField] private PlayerStateView _playerStateView;

        public PlayerStateView PlayerStateView => _playerStateView;
    }
}