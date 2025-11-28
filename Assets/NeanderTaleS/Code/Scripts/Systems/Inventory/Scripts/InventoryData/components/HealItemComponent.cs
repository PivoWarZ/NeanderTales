using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.components
{
    public sealed class HealItemComponent: IItemComponent
    {
        public int Heal;
        public IItemComponent Clone()
        {
            return new HealItemComponent()
            {
                Heal = Heal,
            };
        }
    }
}