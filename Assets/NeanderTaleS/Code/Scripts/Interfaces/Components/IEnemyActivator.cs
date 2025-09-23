namespace NeanderTaleS.Code.Scripts.Interfaces.Components
{
    public interface IEnemyActivator
    {
        float ActivatingDistance { get; }
        void SetActivatingDistance(float distance);
    }
}