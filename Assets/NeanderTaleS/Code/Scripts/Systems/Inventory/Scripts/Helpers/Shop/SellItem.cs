using System;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Helpers.Shop
{
    public sealed class SellItem: MonoBehaviour
    {
        public event Action<GameObject> OnItemSelected;
        public Button Button;
        public Image Image;

        private void Awake()
        {
            Button.onClick.AddListener(ItemSelected);
        }

        private void ItemSelected()
        {
            OnItemSelected?.Invoke(this.gameObject);
        }
    }
}