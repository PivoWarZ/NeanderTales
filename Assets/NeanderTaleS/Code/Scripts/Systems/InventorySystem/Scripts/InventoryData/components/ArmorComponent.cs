using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components
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