using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components
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