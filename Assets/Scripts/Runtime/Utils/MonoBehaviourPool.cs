using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HGtest.Utils
{
    public class MonoBehaviourPool<T> : IDisposable where T : MonoBehaviour
    {
        private readonly T _target;
        private readonly Transform _parent;
        private readonly List<T> _createdObjects;
        private readonly HashSet<T> _busyObjects;

        public MonoBehaviourPool(T objectToCreate, Transform parent)
        {
            _target = objectToCreate;
            _parent = parent;

            _createdObjects = new List<T>(8);
            _busyObjects = new HashSet<T>();
        }

        public T Get()
        {
            var foundObject = _createdObjects.FirstOrDefault(createdObject => !_busyObjects.Contains(createdObject));

            if (foundObject == null)
            {
                foundObject = _createdObjects.FirstOrDefault(createdObject => !_busyObjects.Contains(createdObject)) ?? Object.Instantiate(_target, _parent);
                _createdObjects.Add(foundObject);
            }

            _busyObjects.Add(foundObject);

            foundObject.gameObject.SetActive(true);

            return foundObject;
        }

        public void ReturnToPool(T target)
        {
            target.gameObject.SetActive(false);
            target.transform.SetParent(_parent);
            _busyObjects.Remove(target);
        }

        public void Dispose()
        {
            foreach (var createdObject in _createdObjects)
            {
                if (createdObject != null)
                {
                    Object.Destroy(createdObject.gameObject);
                }
            }

            _createdObjects.Clear();
            _busyObjects.Clear();
        }
    }
}