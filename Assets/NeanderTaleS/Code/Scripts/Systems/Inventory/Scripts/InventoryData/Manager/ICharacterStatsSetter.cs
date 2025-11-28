namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Manager
{
    public interface ICharacterStatsSetter
    {
        void AddHitPoints(int hitPoints);
        void RemoveHitPoints(int hitPoints);
        void AddArmor(int armor);
        void RemoveArmor(int armor);
        void AddDexterity(int dexterity);
        void RemoveDexterity(int dexterity);
        void AddDamage(int damage);
        void RemoveDamage(int damage);
    }
}