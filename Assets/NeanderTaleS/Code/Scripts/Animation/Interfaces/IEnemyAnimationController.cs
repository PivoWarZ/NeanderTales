using NeanderTaleS.Code.Scripts.Animation.PlayerAnimation;
using NeanderTaleS.Code.Scripts.EnemiesComponents;

namespace NeanderTaleS.Code.Scripts.Animation.Interfaces
{
    public interface IEnemyAnimationController
    {
        void Init(EnemyProvider enemyProvider);
    }
}