using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents
{
    public class Player: MonoBehaviour, ICharacterUpgrade
    {
        [SerializeField] private EntityBootsTrap _entityBootsTrap;
        [SerializeField] private LocalProvider localProvider;
        private LastAddedCharacteristics _lastAdded = new ();
        private ITakeDamageable _hitPoints;
        private IStamina _stamina;
        private IAdditionalDamage _additionalDamage;
        private ReactiveProperty<int> _level = new ();
        
        public ReadOnlyReactiveProperty<int> Level => _level;

        public void Init()
        {
            _entityBootsTrap.EntityInitialize();
            
        }

        void ICharacterUpgrade.Upgrade(int level, int health, int stamina, int power)
        {
            var newHealth = health - _lastAdded.Health;
            var newStamina = stamina - _lastAdded.Stamina;
            var newPower = power - _lastAdded.Power;
            
            _level.Value = level;
            _hitPoints.AddedHitPoints(newHealth, newHealth);
            _stamina.AddedStamina(newStamina, newStamina);
            _additionalDamage.AdditionalDamage += newPower;

            _lastAdded.Health = health;
            _lastAdded.Stamina = stamina;
            _lastAdded.Power = power;
        }
    }
}