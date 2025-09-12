namespace NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces
{
    public interface IMachanicsBreaker
    {
        void BanCoreMechanics();
        void ResumeCoreMechanics();
        void BanAttacking();
        void ResumeAttacking();
        void BanMoving();
        void ResumeMoving();
        void BanRotating();
        void ResumeRotating();
    }
}