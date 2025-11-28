using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.components
{
    public sealed class ArmorComponent: IItemComponent
    {
        public int Armor;

        public IItemComponent Clone()
        {
            return new ArmorComponent()
            {
                Armor = Armor
            };
        }
    }
}