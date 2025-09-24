using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Services.Helpers
{
    public class AttackDistanceHelper: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private Weapon _weapon;
        private GameObject _dino;

        private void Start()
        {
            _weapon = _localProvider.GetService<Weapon>(); 
            _dino = _localProvider.gameObject;
        }

        private void Update()
        {
            var distance = (_weapon.transform.position - _dino.transform.position).magnitude;
            Debug.Log(distance);
        }
    }
}