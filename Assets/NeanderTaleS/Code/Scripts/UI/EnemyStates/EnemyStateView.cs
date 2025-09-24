using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.EnemyStates
{
    public class EnemyStateView: MonoBehaviour
    {
        public Image Portrait;
        [SerializeField] private Slider _health;

        private void OnEnable()
        {
            var logo = Resources.Load<Sprite>("DinoLogo");
            Portrait.sprite = logo;
        }

        public void Init(float newValue)
        {
            _health.value = newValue;
        }

        public void SetSlider(float newValue)
        {
            DOTween.To(() => _health.value, x => _health.value = x, newValue, 0.5f);
        }
        
        
    }
}