using UnityEngine;

namespace NeanderTaleS.Code.Configs.Scripts.VelociraptorEnemy
{
    [CreateAssetMenu(fileName = "VelociraptorConfig", menuName = "NeanderTaleS/Enemies/Velociraptor/ New Velociraptor Config")]
    public class VelociraptorConfig: EnemyMetaData
    {
        public int Level = 1;
        public float LevelGain = 0.1f;
        [SerializeField, Range(0.5f, 2f)] private float _size = 0.5f;
        
        public float HitPoints;
        public float Damage;
        public float Speed;
        public float StunChance;
        public float PushPower;
        public float SizeCoefficient => Mathf.InverseLerp(0f, 2f, Size);

        public float Size => _size;

        private void OnValidate()
        {
            var gain = (Level - 1) * LevelGain;
            
            HitPoints = BaseHitPoints * SizeCoefficient + BaseHitPoints * SizeCoefficient * gain;
            Damage = BaseDamage * SizeCoefficient + BaseDamage * SizeCoefficient * gain;
            Speed = Mathf.Max(4, BaseSpeed - BaseSpeed * (1 - SizeCoefficient) * 0.5f);
            StunChance = Mathf.Max(BaseStunChance - BaseStunChance * (1 - SizeCoefficient) * 1.5f, 0f);
            PushPower = BasePushPower * SizeCoefficient;
        }

        public void SetSize(float size)
        {
            _size = size;
        }
    }
}