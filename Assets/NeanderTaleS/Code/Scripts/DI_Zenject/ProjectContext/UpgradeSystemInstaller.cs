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
using NeanderTaleS.Code.Scripts.UI.Bus;
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
            
            BindPlayerStatsInstaller();
            
            Container.BindInterfacesAndSelfTo<InstantiatePlayerEventObserver_CreateModelViewPresenter_UI>()
                .AsCached()
                .NonLazy();
            
            //Bus
            
            Container.BindInterfacesAndSelfTo<UpgradeConstructedHandler_BindUpgrade>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<InstantiatePlayerObserver_InitializeStatsUpgradeInstaller>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SpendCoinsRequestHandler>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UpdateCoinsListener_UpgradeButtonVisibility>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<SystemLevelUpEventListener_RiseLevelUpEvent>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<LevelUpRequestObserver_CanLevelUp>()
                .AsCached()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<CharacterUpgradeConstructListener_RiseLevelUpEvent>()
                .AsCached();
            
            Container.BindInterfacesAndSelfTo<UpgradeBoxCreator>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<InstantiateCharacterEventObserver_ConstructCharacterUpgrade>()
                .AsCached()
                .NonLazy();
            
            DebugLogger.PrintBinding(this);
        }
        
        private void BindStatsUpgradeInstaller()
        {
            Container.BindInterfacesAndSelfTo<StatsUpgradesSystemInitializer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradeManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StatsUpgradePopupsInstaller>().AsSingle().NonLazy();
        }

        private void BindPlayerStatsInstaller()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatsInstaller>().AsSingle().NonLazy();
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