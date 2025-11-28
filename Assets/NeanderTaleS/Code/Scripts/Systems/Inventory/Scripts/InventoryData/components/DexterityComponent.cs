using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components
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