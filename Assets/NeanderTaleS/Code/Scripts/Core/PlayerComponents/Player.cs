using NeanderTaleS.Code.Scripts.Core.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private EntityBootsTrap _entityBootsTrap;

        private void Awake()
        {
            _entityBootsTrap.EntityInitialize();
        }
    }
}