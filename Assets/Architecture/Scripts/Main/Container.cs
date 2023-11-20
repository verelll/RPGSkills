using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGSkills.Architecture
{
    public sealed class Container
    {
        private Dictionary<Type, Shared> _sharedByTypes;

        public bool Initialized { get; private set; }
        public event Action OnInitialized;
        
        public Container()
        {
            _sharedByTypes = new Dictionary<Type, Shared>();
        }

        public void AddShared<T>() where T : Shared, new()
        {
            var newShared = new T();
            var type = typeof(T);

            if (_sharedByTypes.ContainsKey(type))
            {
                Debug.LogError($"Shared with type: {type} already exist!");
                return;
            }

            _sharedByTypes[type] = newShared;
        }

        public T GetShared<T>() where T: Shared
        {
            var type = typeof(T);
            if (!_sharedByTypes.TryGetValue(type, out var shared))
                return default;

            return shared as T;
        }
        
        public void InjectAt(IInjected injectedObject)
        {
            injectedObject?.SetContainer(this);
        }

        public void InitShared()
        {
            foreach (var pair in _sharedByTypes)
            {
                var shared = pair.Value;
                InjectAt(shared);
            }
            
            foreach (var pair in _sharedByTypes)
            {
                var shared = pair.Value;
                shared.PreInit();
            }
            
            foreach (var pair in _sharedByTypes)
            {
                var shared = pair.Value;
                shared.Init();
            }
            
            foreach (var pair in _sharedByTypes)
            {
                var shared = pair.Value;
                shared.AfterInit();
            }
        }

        public void DisposeShared()
        {
            foreach (var pair in _sharedByTypes)
            {
                var shared = pair.Value;
                shared.Dispose();
            }
        }
    }
}