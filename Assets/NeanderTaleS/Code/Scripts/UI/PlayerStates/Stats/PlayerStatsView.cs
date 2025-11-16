using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.Stats
{
    public sealed class PlayerStatsView: MonoBehaviour
    {
        [SerializeField] private Slider _health;
        [SerializeField] private Slider _stamina;

        private void Awake()
        {
            _health.interactable = false;
            _stamina.interactable = false;
        }

        public void SetStamina(float newValue)
        {
            DOTween.To(() => _stamina.value, x => _stamina.value = x, newValue, 1f);
        }

        public void SetHealth(float newValue)
        {
            DOTween.To(() => _health.value, x => _health.value = x, newValue, 1f);
        }
    }
}