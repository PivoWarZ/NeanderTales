namespace NeanderTaleS.Code.Scripts.Systems.SaveLoad.Context
{
    public interface IContext
    {
        T GetService<T>();
    }
}