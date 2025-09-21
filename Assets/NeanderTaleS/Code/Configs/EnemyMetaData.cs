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
        public float BaseStunChance;
        public float BasePushPower;
    }
}