namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IAdditionalDamage
    {
        void AddDamage(ref float damage);
        float AdditionalPercentDamage { get; set; }
    }
}