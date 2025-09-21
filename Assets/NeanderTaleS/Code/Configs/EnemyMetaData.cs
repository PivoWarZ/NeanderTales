using System;
using UnityEngine;

namespace NeanderTaleS.Code.Configs
{
    public class EnemyMetaData: ScriptableObject
    {
        public Sprite Logo;
        public float BaseHitPoints;
        public float BaseDamage;
        public float BaseSpeed;
        public float BaseAttackDistance;
        public float BaseActivatingDistance;
        public float BaseStunChance;
        public float BasePushPower;
    }
}