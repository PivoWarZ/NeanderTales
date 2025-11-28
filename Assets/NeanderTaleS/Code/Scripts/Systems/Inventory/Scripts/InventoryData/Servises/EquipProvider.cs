using System;
using UnityEngine;

namespace Inventory.Scripts.InventoryData.Servises
{
    [Serializable]
    public sealed class EquipProvider
    {
        [field: SerializeField] public GameObject SwordPoint { get; private set;}
        [field: SerializeField] public GameObject LegsPoint { get; private set;}
        [field: SerializeField] public GameObject HelmetPoint { get; private set;}
        [field: SerializeField] public GameObject ShieldPoint { get; private set;}
    }
}