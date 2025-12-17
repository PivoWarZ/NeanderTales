using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class BagsProvider: MonoBehaviour
    {
        [SerializeField] private Image _background;

        public Image Background => _background;
    }
}