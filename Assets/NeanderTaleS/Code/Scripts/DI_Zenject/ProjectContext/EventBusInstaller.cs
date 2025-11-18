using NeanderTaleS.Code.Scripts.Core.Services.Helpers;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.Experience.ExperienceStorage.Bus;
using NeanderTaleS.Code.Scripts.Systems.Factory;
using NeanderTaleS.Code.Scripts.Systems.GameCycle.Bus;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Bus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Bus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Bus;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management.Bus;
using NeanderTaleS.Code.Scripts.UI.Bus;
using Zenject;

namespace NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext
{
    public sealed class EventBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle().NonLazy();

            BindUIObservers();
            
            BindExperienceSystemObservers();
            
            BindGameCycleObservers();
            
            BindCamerasObserver();
            
            BindInputSystemObserver();
            
            BindUpgradeSystemObservers();
            
            DebugLogger.PrintBinding(this);
        }

        private void BindUpgradeSystemObservers()
        {
            Container.BindInterfacesAndSelfTo<InstantiateCharacterEventObserver_ConstructCharacterUpgrade>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<LevelUpRequestObserver_CanLevelUp>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<InstantiatePlayerObserver_InitializeStatsUpgradeInstaller>()
                .AsCached()
                .NonLazy();
        }

        private void BindInputSystemObserver()
        {
            Container.BindInterfacesAndSelfTo<InstantiatePlayerObserver_ConstructInputSystem>()
                .AsCached()
                .NonLazy();
        }

        private void BindCamerasObserver()
        {
            Container.BindInterfacesAndSelfTo<InstantiatePlayerEventObserver_InitializeCamerasProvider>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameCycleObservers()
        {
            Container.BindInterfacesAndSelfTo<GameCycleEventsObserver_SetGameState>()
                .AsCached()
                .NonLazy();
        }

        private void BindExperienceSystemObservers()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnedEventObserver_TryAddExperienceDealer>()
                .AsCached()
                .NonLazy();
        }

        private void BindUIObservers()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawnedEventObserver_AddDamageable>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UpgradePopupActiveListener_RisePauseResumeRequests>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<InstantiatePlayerEventObserver_CreateModelViewPresenter_UI>()
                .AsCached()
                .NonLazy();
        }
    }
}