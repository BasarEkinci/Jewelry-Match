using UnityEngine;

namespace _GameFolders.Scripts.Functionaries
{
    public class VibrationHelper
    {
        public static void Vibrate(long milliseconds)
        {
            if (!GameSettings.isVibrationOn)
            {
                return;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        Handheld.Vibrate();
#else
            Debug.Log("Vibration!");
#endif
        }
    }
}