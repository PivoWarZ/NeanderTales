using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents
{
    public class Player: MonoBehaviour, IUpgradePlayer
    {
        [SerializeField] private EntityBootsTrap _entityBootsTrap;
        [SerializeField] private LocalProvider _localProvider;
        private IHitPointsComponent _hitPoints;
        private IStaminaComponent _staminaComponent;
        private IAdditionalDamage _additionalDamage;
        [SerializeField]private SerializableReactiveProperty<int> _level = new ();
        
        public ReadOnlyReactiveProperty<int> Level => _level;

        public void Awake()
        {
            _entityBootsTrap.EntityInitialize();
            _hitPoints = _localProvider.GetInterface<IHitPointsComponent>();
            _staminaComponent = _localProvider.GetInterface<IStaminaComponent>();
            _additionalDamage = _localProvider.GetInterface<IAdditionalDamage>();
        }

        void IUpgradePlayer.Upgrade(int level, int health, int stamina, int power)
        {
            Debug.Log($"Level : {level}, Health: {health}, Stamina: {stamina}, Power: {power}");
            _level.Value = level;
            _hitPoints.SetHitPoints(health, health);
            _staminaComponent.SetStamina(stamina, stamina);
            _additionalDamage.AdditionalPercentDamage += power;
        }
    }
}