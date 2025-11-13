using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents
{
    public class Player: MonoBehaviour, IUpgradePlayer
    {
        [SerializeField] private SerializableReactiveProperty<int> _level = new ();
        [SerializeField] private EntityBootsTrap _entityBootsTrap;
        [SerializeField] private LocalProvider _localProvider;
        private IHitPointsComponent _hitPoints;
        private IStaminaComponent _staminaComponent;
        private IAdditionalDamage _additionalDamage;
        private Stats _currentStats;
        private const int FIRST_LEVEL = 1;
        
        public ReadOnlyReactiveProperty<int> Level => _level;

        public void Awake()
        {
            _entityBootsTrap.EntityInitialize();
            _hitPoints = _localProvider.GetInterface<IHitPointsComponent>();
            _staminaComponent = _localProvider.GetInterface<IStaminaComponent>();
            _additionalDamage = _localProvider.GetInterface<IAdditionalDamage>();
        }

        void IUpgradePlayer.Upgrade(Stats stats)
        {
            if (stats.Level == FIRST_LEVEL)
            {
                SetStats(stats);
            }
            else
            {
                Stats newStat = GetStatsDifference(stats);
            
                AddStats(newStat);
            }
            
            _currentStats = stats;
        }

        private void SetStats(Stats stats)
        {
            _level.Value = stats.Level;
            _hitPoints.SetHitPoints(stats.Health, stats.Health);
            _staminaComponent.SetStamina(stats.Stamina, stats.Stamina);
            _additionalDamage.AdditionalPercentDamage += stats.Power;
        }

        private Stats GetStatsDifference(Stats stats)
        {
            Stats statsDifference = new()
            {
                Level = stats.Level,
                Health = stats.Health - _currentStats.Health,
                Stamina = stats.Stamina - _currentStats.Stamina,
                Power = stats.Power
            };

            return statsDifference;
        }

        private void AddStats(Stats stats)
        {
            _level.Value = stats.Level;
            _hitPoints.AddHitPoints(stats.Health, stats.Health);
            _staminaComponent.AddMaxStamina(stats.Stamina);
            _additionalDamage.AdditionalPercentDamage += stats.Power;
        }
    }
}