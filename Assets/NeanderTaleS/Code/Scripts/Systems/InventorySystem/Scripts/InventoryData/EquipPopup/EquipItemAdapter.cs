using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.EquipPopup
{
    public sealed class EquipItemAdapter
    {
        private readonly EquipPopupView _view;

        public EquipItemAdapter(EquipPopupView view)
        {
            _view = view;
        }

        public void EquipItem(InventoryItem item)
        {
            _view.EuipItem(item);
        }
    }
}