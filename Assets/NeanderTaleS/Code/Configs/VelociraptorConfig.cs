using UnityEngine;

namespace NeanderTaleS.Code.Configs
{
    [CreateAssetMenu(fileName = "VelociraptorConfig", menuName = "NeanderTales/Enemies/Velociraptor/ New Velociraptor Config")]
    public class VelociraptorConfig: EnemyMetaData
    {
        
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
            HitPoints = BaseHitPoints * SizeCoefficient;
            Damage = BaseDamage * SizeCoefficient;
            Speed = Mathf.Max(4, BaseSpeed - BaseSpeed * (1 - SizeCoefficient) * 0.5f);
            StunChance = Mathf.Max(BaseStunChance - BaseStunChance * (1 - SizeCoefficient) * 1.5f, 0f);
            PushPower = BasePushPower * SizeCoefficient;
        }
    }
}