using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI
{
    public class HudUI: MonoBehaviour
    {
        [SerializeField] private PlayerStateView _playerStateView;
        [SerializeField] private EnemyStateView _enemyStates;
        public PlayerStateView PlayerStateView => _playerStateView;

        public EnemyStateView EnemyStatesView => _enemyStates;
    }
}