using Inventory.Scripts.InventoryData.InventoryBase;

namespace Inventory.Scripts.InventoryData.EquipPopup
{
    public sealed class EquipItemAdapter
    {
        private readonly EquipPopupView _view;
        private string _bodyPart;

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