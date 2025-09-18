using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.UIScripts
{
    public class PlayerStateView: MonoBehaviour
    {
        public Image Portrait;
        public TMP_Text Name;
        public TMP_Text Level;
        public Slider Health;
        public Slider Stamina;
        public Slider Experience;
        public Button LevelUpMarker;
    }
}