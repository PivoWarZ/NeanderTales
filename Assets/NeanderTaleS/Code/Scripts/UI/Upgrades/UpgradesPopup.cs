using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class UpgradesPopup: MonoBehaviour
    {
        [SerializeField] Transform _content;
        [SerializeField] Button _closeButton;
        [SerializeField] TMP_Text _starsCount;

        public Transform Content => _content;

        public Button CloseButton => _closeButton;
    }
}