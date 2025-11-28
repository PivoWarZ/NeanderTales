using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components
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