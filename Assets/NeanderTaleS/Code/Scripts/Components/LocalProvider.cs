using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class LocalProvider: MonoBehaviour
    {
        private List<MonoBehaviour> _components;

        private void Awake()
        {
            _components = gameObject.GetComponentsInChildren<MonoBehaviour>().ToList();
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            return _components.OfType<T>().FirstOrDefault();
        }
    }
}