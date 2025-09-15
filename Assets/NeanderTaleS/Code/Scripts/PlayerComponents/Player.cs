using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents
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