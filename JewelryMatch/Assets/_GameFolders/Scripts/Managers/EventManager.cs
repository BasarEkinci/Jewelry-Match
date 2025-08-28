using System;
using System.Collections.Generic;

namespace _GameFolders.Scripts.Managers
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, Delegate> Events = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            if (Events.TryGetValue(typeof(T), out var existing))
            {
                Events[typeof(T)] = Delegate.Combine(existing, callback);
            }
            else
            {
                Events[typeof(T)] = callback;
            }
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            if (Events.TryGetValue(typeof(T), out var existing))
            {
                var newDelegate = Delegate.Remove(existing, callback);
                if (newDelegate == null)
                    Events.Remove(typeof(T));
                else
                    Events[typeof(T)] = newDelegate;
            }
        }

        public static void Publish<T>(T signal)
        {
            if (Events.TryGetValue(typeof(T), out var existing))
            {
                (existing as Action<T>)?.Invoke(signal);
            }
        }
    }
}