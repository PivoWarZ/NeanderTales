using UnityEngine;

namespace NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy
{
    public class EnemyMetaData: ScriptableObject
    {
        public Sprite Logo;
        public float BaseHitPoints;
        public float BaseDamage;
        public float BaseSpeed;
        public float BaseStunChance;
        public float BasePushPower;
        public float BaseExperience;
    }
}