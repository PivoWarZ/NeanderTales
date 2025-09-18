using NeanderTaleS.Code.CoreScripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.PlayerComponents
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