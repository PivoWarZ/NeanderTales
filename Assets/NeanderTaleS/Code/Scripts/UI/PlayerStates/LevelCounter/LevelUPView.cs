using TMPro;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.UI.PlayerStates.LevelCounter
{
    public sealed class LevelUPView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;

        public void SetLevel(int level)
        {
            _level.text = level.ToString();
        }
    }
}