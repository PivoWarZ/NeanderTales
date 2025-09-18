using NeanderTaleS.Code.CoreScripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Effects.HitFX
{
    public class OnHitFX: MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _scale;
        [SerializeField] private PointHitDamageListener _hitDamage;
        private ParticleSystem _effect;

        private void Awake()
        {
            _particle.playOnAwake = false;
            _effect = Instantiate(_particle, transform).GetComponent<ParticleSystem>();
            _effect.gameObject.transform.localScale *= _scale;
            
            _hitDamage.OnHitPoint += PlayEffect;
        }

        private void PlayEffect(Vector3 position)
        {
            _effect.transform.position = position;
            _effect.Play();
        }
    }
}