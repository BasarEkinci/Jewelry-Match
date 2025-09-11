using System;
using _GameFolders.Scripts.Extensions;

namespace _GameFolders.Scripts.UI
{
    public class SettingsButtonManager : MonoSingleton<SettingsButtonManager>
    { 
        public bool IsMusicOn { get; private set; }
        public bool IsSfxOn { get; private set; }
        public bool IsVibrationOn { get; private set; }

        private void Start()
        {
            if (!GameDatabase.HasKey(Constants.IsMusicOnKey))
                GameDatabase.SaveData(Constants.IsMusicOnKey, true);
            if (!GameDatabase.HasKey(Constants.IsSfxOnKey))
                GameDatabase.SaveData(Constants.IsSfxOnKey,  true);
            if (!GameDatabase.HasKey(Constants.IsVibrationOnKey))
                GameDatabase.SaveData(Constants.IsVibrationOnKey,  true);
        }

        private void OnEnable()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            IsMusicOn = GameDatabase.LoadData<bool>(Constants.IsMusicOnKey);
            IsSfxOn = GameDatabase.LoadData<bool>(Constants.IsSfxOnKey);
            IsVibrationOn = GameDatabase.LoadData<bool>(Constants.IsVibrationOnKey);
        }

        public void SetMusic(bool value)
        {
            IsMusicOn = value;
            GameDatabase.SaveData(Constants.IsMusicOnKey, IsMusicOn);
        }

        public void SetSfx(bool value)
        {
            IsSfxOn = value;
            GameDatabase.SaveData(Constants.IsSfxOnKey, IsSfxOn);
        }

        public void SetVibration(bool value)
        {
            IsVibrationOn = value;
            GameDatabase.SaveData(Constants.IsVibrationOnKey, IsVibrationOn);
        }
    }
}
