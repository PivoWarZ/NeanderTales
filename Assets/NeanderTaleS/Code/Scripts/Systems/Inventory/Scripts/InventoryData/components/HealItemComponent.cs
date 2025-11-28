using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components
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