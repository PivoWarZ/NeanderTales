using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy
{
    public class EnemyMetaConfigs: ScriptableObject
    {
        public Enemy Prefab;
        public Sprite Logo;
        public float BaseHitPoints;
        public float BaseDamage;
        public float BaseSpeed;
        public float BaseStunChance;
        public float BasePushPower;
        public float BaseExperience;
    }
}