namespace Inventory.Scripts.DI
{
    public interface IContext
    {
        T GetService<T>();
    }
}