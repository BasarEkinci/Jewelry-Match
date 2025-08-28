using System;
using UnityEngine;

namespace _GameFolders.Scripts.Extensions
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => _instance.Value;

        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var existingInstance = FindObjectOfType<T>();
            if (existingInstance != null)
                return existingInstance;
        
            GameObject singletonObject = new GameObject(typeof(T).Name);
            return singletonObject.AddComponent<T>();
        });

        protected virtual void Awake()
        {
            if (_instance.IsValueCreated && _instance.Value != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}