using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Bus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Health;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Power;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Stamina;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.EntryPoint;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management.Bus;
using NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class UpgradeSystemInstaller: MonoInstaller
    {
        [SerializeField] private CharacterUpgradesConfig _characterConfig;
        [SerializeField] private HealthUpgradeConfig _healthConfig;
        [SerializeField] private StaminaUpgradeConfig _staminaConfig;
        [SerializeField] private PowerUpgradeConfig _powerConfig;
        
        public override void InstallBindings()
        {
            BindCharacterUpgrade();

            BindStatsUpgradeInstaller();
            
            //UI
            
            BindPlayerStatsModelViewPresenterInstaller();
            
            //Bus
            
            BindUpgradeBoxCreator();
            
            BindHandlers();

            BindListeners();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindHandlers()
        {
            Container.BindInterfacesAndSelfTo<UpgradeConstructedHandler_BindUpgrade>()
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<SpendCoinsRequestHandler>()
                .AsCached()
                .NonLazy();
        }

        private void BindListeners()
        {
            Container.BindInterfacesAndSelfTo<UpdateCoinsListener_UpgradeButtonVisibility>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<SystemLevelUpEventListener_RiseLevelUpEvent>()
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<CharacterUpgradeConstructListener_RiseLevelUpEvent>()
                .AsCached();
        }

        private void BindUpgradeBoxCreator()
        {
            Container.BindInterfacesAndSelfTo<UpgradeBoxCreator>()
                .AsCached()
                .NonLazy();
        }

        private void BindStatsUpgradeInstaller()
        {
            Container.BindInterfacesAndSelfTo<StatsUpgradesSystemInitializer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradeManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradePopupsInstaller>().AsSingle().NonLazy();
        }

        private void BindPlayerStatsModelViewPresenterInstaller()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatsModelViewPresenterInstaller>()
                .AsCached()
                .NonLazy();
        }

        private void BindCharacterUpgrade()
        {
            Container.BindInstance(_characterConfig);
            
            CharacterUpgrade characterUpgrade = new CharacterUpgrade(_characterConfig);
            Container.Bind<Upgrade>().FromInstance(characterUpgrade).AsCached();
            Container.BindInstance(characterUpgrade).AsSingle();

            
            Container.BindInterfacesAndSelfTo<CharacterUpgradeSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}