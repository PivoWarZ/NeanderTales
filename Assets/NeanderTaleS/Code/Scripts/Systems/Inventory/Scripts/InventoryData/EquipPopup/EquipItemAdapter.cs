using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup
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