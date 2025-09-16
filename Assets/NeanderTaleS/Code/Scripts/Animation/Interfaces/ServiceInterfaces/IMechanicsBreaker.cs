namespace NeanderTaleS.Code.Scripts.Animation.Interfaces.ServiceInterfaces
{
    public interface IMechanicsBreaker
    {
        void BanCoreMechanics();
        void EnabledCoreMechanics();
        void BanAttacking();
        void ResumeAttacking();
        void BanMoving();
        void ResumeMoving();
        void BanRotating();
        void ResumeRotating();
    }
}