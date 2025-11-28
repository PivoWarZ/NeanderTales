using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.components
{
    public sealed class DamageItemComponent: IItemComponent
    {
        public int Damage;

        public IItemComponent Clone()
        {
            return new DamageItemComponent()
            {
                Damage = Damage
            };
        }
    }
}