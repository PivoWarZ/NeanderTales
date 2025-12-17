using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components
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