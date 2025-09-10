using System;
using UnityEngine;
#pragma warning disable CS0618 // Type or member is obsolete

namespace _GameFolders.Scripts.Extensions
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => instance.Value;

        private static readonly Lazy<T> instance = new Lazy<T>(() =>
        {
            var existingInstance = FindObjectOfType<T>();
            if (existingInstance != null)
                return existingInstance;
        
            GameObject singletonObject = new GameObject(typeof(T).Name);
            return singletonObject.AddComponent<T>();
        });

        protected virtual void Awake()
        {
            if (instance.IsValueCreated && instance.Value != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}