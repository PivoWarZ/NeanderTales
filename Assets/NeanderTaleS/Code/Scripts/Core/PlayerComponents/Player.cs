using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents
{
    public class Player: MonoBehaviour, ICharacterUpgrade
    {
        [SerializeField] private EntityBootsTrap _entityBootsTrap;
        [SerializeField] private LocalProvider _localProvider;
        private LastAddedCharacteristics _lastAdded = new ();
        private ITakeDamageable _hitPoints;
        private IStamina _stamina;
        private IAdditionalDamage _additionalDamage;
        private ReactiveProperty<int> _level = new ();
        
        public ReadOnlyReactiveProperty<int> Level => _level;

        public void Init()
        {
            _entityBootsTrap.EntityInitialize();
            _hitPoints = _localProvider.GetInterface<ITakeDamageable>();
            _stamina = _localProvider.GetInterface<IStamina>();
            _additionalDamage = _localProvider.GetInterface<IAdditionalDamage>();
        }

        void ICharacterUpgrade.Upgrade(int level, int health, int stamina, int power)
        {
            if (level == 1)
            {
                SetFirstLevel(level, health, stamina, power);
                return;
            }

            Debug.Log("Upgrade level " + level);
            var newHealth = health - _lastAdded.Health;
            var newStamina = stamina - _lastAdded.Stamina;
            var newPower = power - _lastAdded.Power;
            
            _level.Value = level;
            _hitPoints.AddedtHitPoints(newHealth, newHealth);
            _hitPoints.AddedtHitPoints(_hitPoints.MaxHitPoints.CurrentValue);
            _stamina.AddedStamina(newStamina, newStamina);
            _additionalDamage.AdditionalPercentDamage += newPower;

            _lastAdded.Health = health;
            _lastAdded.Stamina = stamina;
            _lastAdded.Power = power;
        }

        private void SetFirstLevel(int level, int health, int stamina, int power)
        {
            _level.Value = level;
            _hitPoints.AddedtHitPoints(health, health);
            _stamina.AddedStamina(stamina, stamina);
            _additionalDamage.AdditionalPercentDamage += power;
            
            _lastAdded.Health = health;
            _lastAdded.Stamina = stamina;
            _lastAdded.Power = power;
        }
    }
}