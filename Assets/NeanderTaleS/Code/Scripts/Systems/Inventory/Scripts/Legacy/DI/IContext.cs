namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.DI
{
    public interface IContext
    {
        T GetService<T>();
    }
}