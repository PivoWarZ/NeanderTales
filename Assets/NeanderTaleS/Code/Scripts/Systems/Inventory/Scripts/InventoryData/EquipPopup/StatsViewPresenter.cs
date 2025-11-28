using System;
using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.EquipPopup
{
    public sealed class StatsViewPresenter: IDisposable, IInventoryInitializable
    {
        private ICharacterStatsStructure _stats;
        private StatsView _statsView;

        public StatsViewPresenter(StatsView statsView)
        {
            _statsView = statsView;
        }
        
        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _stats = inventoryComponent.CharacterStatsStructure;
            _stats.OnStatsValueChanged += UpdateStats;
            UpdateStats();
        }

        private void UpdateStats()
        {
            var stats = _stats.GetStats();
            _statsView.Init(stats);
        }

        public void Dispose()
        {
            _stats.OnStatsValueChanged -= UpdateStats;
        }
    }
}