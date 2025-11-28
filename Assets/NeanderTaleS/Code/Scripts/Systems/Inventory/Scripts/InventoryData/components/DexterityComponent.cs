using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.components
{
    public sealed class DexterityComponent: IItemComponent
    {
        public int Dexterity;
        
        public IItemComponent Clone()
        {
            
            return new DexterityComponent()
            {
                Dexterity = Dexterity
            };
        }
    }
}