using System;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem;
using NeanderTaleS.Code.Scripts.Systems.EventBusSystem.Events.GameCyclesEvents;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management.Bus
{
    public class UpgradePopupActiveListener_RisePauseResumeRequests: IInitializable, IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly StatsUpgradeManager _statsUpgradeManager;

        public UpgradePopupActiveListener_RisePauseResumeRequests(IEventBus eventBus, StatsUpgradeManager statsUpgradeManager)
        {
            _eventBus = eventBus;
            _statsUpgradeManager = statsUpgradeManager;
        }

        void IInitializable.Initialize()
        {
            _statsUpgradeManager.OnUpgradePopupActive += RiseEvents;
        }

        private void RiseEvents(bool isActive)
        {
            if (isActive)
            {
                _eventBus.RiseEvent(new PauseGameRequestEvent(GetType().Name));
            }
            else
            {
                _eventBus.RiseEvent(new ResumeGameRequestEvent(GetType().Name));
            }
        }

        void IDisposable.Dispose()
        {
            _statsUpgradeManager.OnUpgradePopupActive -= RiseEvents;
        }
    }
}