using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components
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