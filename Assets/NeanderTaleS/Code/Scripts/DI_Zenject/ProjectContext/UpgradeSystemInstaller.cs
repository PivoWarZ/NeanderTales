using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using NeanderTaleS.Code.Scripts.UI.PlayerStates;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public class UpgradeSystemInstaller: MonoInstaller
    {
        [SerializeField] private CharacterUpgradesConfig _characterConfig;
        [SerializeField] private HealthUpgradeConfig _healthConfig;
        [SerializeField] private StaminaUpgradeConfig _staminaConfig;
        [SerializeField] private PowerUpgradeConfig _powerConfig;
        
        public override void InstallBindings()
        {
            BindExperienceStorage();

            BindCharacterUpgrade();

            Container.BindInterfacesAndSelfTo<PlayerStatsInstaller>().AsSingle().NonLazy();
            
            HealthUpgrade healthUpgrade = new HealthUpgrade(_healthConfig);
            Container.Bind<Upgrade>().FromInstance(healthUpgrade).AsCached();
            
            StaminaUpgrade staminaUpgrade = new StaminaUpgrade(_staminaConfig);
            Container.Bind<Upgrade>().FromInstance(staminaUpgrade).AsCached();
            
            PowerUpgrade powerUpgrade = new PowerUpgrade(_powerConfig);
            Container.Bind<Upgrade>().FromInstance(powerUpgrade).AsCached();
            
            Container.Bind<StatsUpgradesInstaller>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradeManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradePopupInstaller>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<LevelUpListener_RewardCoins>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<StarsCountAdapter>().AsSingle().NonLazy();
            
            Debug.Log($"Binding {GetType().Name}");
        }

        private void BindCharacterUpgrade()
        {
            Container.BindInstance(_characterConfig);
            Container.Bind<CharacterUpgrade>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<CharacterUpgradesSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindExperienceStorage()
        {
            Container.BindInterfacesAndSelfTo<ExperienceStorage>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ExperienceSystem>()
                .AsSingle()
                .NonLazy();
        }
        
    }
}