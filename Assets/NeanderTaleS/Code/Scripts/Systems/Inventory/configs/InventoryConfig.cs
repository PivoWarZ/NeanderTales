using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.configs
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "Inventory/Config/New InventoryConfig")]
    public class InventoryConfig: ScriptableObject
    {
        [Header("------Inventory size------")]
        [SerializeField, Range (1, 10)] private int _widthCount;
        [SerializeField, Range (1, 10)] private int _heightCount;
        [ShowInInspector] private int _capacity;

        [Header("------Grid settings------")] 
        [SerializeField] private GridItem _grid;
        [SerializeField] private float _bagsContentPadding;

        public int Capacity => _capacity;

        public int WidthCount => _widthCount;

        public int HeightCount => _heightCount;

        public GridItem Grid => _grid;

        public float BagsContentPadding => _bagsContentPadding;


        private void OnValidate()
        {
            _capacity = WidthCount * HeightCount;
        }
    }
}