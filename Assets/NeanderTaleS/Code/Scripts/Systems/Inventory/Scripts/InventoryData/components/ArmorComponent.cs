using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components
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