using NeanderTaleS.Code.Scripts.UI.EnemyStates;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Experience;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.LevelCounter;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Logo;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI
{
    public sealed class HudUI: MonoBehaviour
    {
        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private EnemyStateView _enemyStates;
        [SerializeField] private UpgradesPopup _upgradesPopup;
        [SerializeField] private LevelUPView _levelUpView;
        [SerializeField] private ExperienceView _experienceView;
        [SerializeField] private LogoView _logoView;
        public PlayerStatsView PlayerStatsView => _playerStatsView;

        public EnemyStateView EnemyStatesView => _enemyStates;

        public UpgradesPopup UpgradesPopup => _upgradesPopup;

        public LevelUPView LevelUpView => _levelUpView;

        public ExperienceView ExperienceView => _experienceView;

        public LogoView LogoView => _logoView;
    }
}