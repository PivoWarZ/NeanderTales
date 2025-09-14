using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class LocalProvider: MonoBehaviour
    {
        private List<MonoBehaviour> _components;
        private Animator _animator;
        private Rigidbody _rigidbody;

        public Animator Animator => _animator;

        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _components = gameObject.GetComponentsInChildren<MonoBehaviour>().ToList();
            _animator = gameObject.GetComponentInChildren<Animator>();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public T GetService<T>() where T : MonoBehaviour
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public bool TryGetService<T>(out T service) where T : MonoBehaviour
        {
            service = GetService<T>();
            
            return service != null;
        }

        public T GetInterface<T>() where T : class
        {
            foreach (var component in _components)
            {
                if (component is T tComponent)
                {
                    return tComponent;
                }
            }

            return null;
        }
        
        public List<T> GetInterfaces<T>() where T : class
        {
            var list = new List<T>();
            
            foreach (var component in _components)
            {
                if (component is T tComponent)
                {
                    list.Add(tComponent);
                }
            }

            if (list.Count == 0)
            {
                return null;
            }
            
            return list;
        }
    }
}