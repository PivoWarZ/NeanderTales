using DG.Tweening;
using NeanderTaleS.Code.Scripts.Interfaces.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates
{
    public class PlayerStateView: MonoBehaviour, IPlayerStateSliders
    {
        public Image Portrait;
        public TMP_Text Name;
        public TMP_Text Level;
        public Slider Health;
        public Slider Stamina;
        public Slider Experience;
        public Button LevelUpMarker;

        private void Awake()
        {
            Health.interactable = false;
            Stamina.interactable = false;
            Experience.interactable = false;
        }

        public void SetExperience(float newValue)
        {
            DOTween.To(() => Experience.value, x => Experience.value = x, newValue, 0.5f);
        }

        public void SetStamina(float newValue)
        {
            DOTween.To(() => Stamina.value, x => Stamina.value = x, newValue, 0.5f);
        }

        public void SetHealth(float newValue)
        {
            DOTween.To(() => Health.value, x => Health.value = x, newValue, 0.5f);
        }
    }
}