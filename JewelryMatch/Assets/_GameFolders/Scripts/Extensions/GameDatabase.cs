using System;
using UnityEngine;

namespace _GameFolders.Scripts.Extensions
{
    public static class GameDatabase
    {
        public static void SaveData<T>(string key, T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(key, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(key, s);
                    break;
                case bool b:
                    PlayerPrefs.SetInt(key, b ? 1 : 0);
                    break;
                default:
                    throw new Exception("Unsupported type for PlayerPrefs");
            }
            PlayerPrefs.Save();
        }

        public static T LoadData<T>(string key, T defaultValue = default)
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            object result = defaultValue;

            if (typeof(T) == typeof(int))
                result = PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
            else if (typeof(T) == typeof(float))
                result = PlayerPrefs.GetFloat(key, Convert.ToSingle(defaultValue));
            else if (typeof(T) == typeof(string))
                result = PlayerPrefs.GetString(key, Convert.ToString(defaultValue));
            else if (typeof(T) == typeof(bool))
                result = PlayerPrefs.GetInt(key, (bool)(object)defaultValue ? 1 : 0) == 1;
            else
                throw new Exception("Unsupported type for PlayerPrefs");

            return (T)result;
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        public static void DeleteData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
            }
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}