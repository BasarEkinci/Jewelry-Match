using _GameFolders.Scripts.Functionaries;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI
{
    public class ToggleHandler : MonoBehaviour
    {
        public enum SettingType { Music, SFX, Vibration }

        [SerializeField] private Toggle toggle;
        [SerializeField] private SettingType settingType;

        private void OnEnable()
        {
            switch (settingType)
            {
                case SettingType.Music:
                    toggle.isOn = SettingsButtonManager.Instance.IsMusicOn;
                    break;
                case SettingType.SFX:
                    toggle.isOn = SettingsButtonManager.Instance.IsSfxOn;
                    break;
                case SettingType.Vibration:
                    toggle.isOn = SettingsButtonManager.Instance.IsVibrationOn;
                    break;
            }

            toggle.onValueChanged.AddListener(OnToggleChanged);
        }
        
        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }

        private void OnToggleChanged(bool isOn)
        {
            switch (settingType)
            {
                case SettingType.Music:
                    SettingsButtonManager.Instance.SetMusic(isOn);
                    break;
                case SettingType.SFX:
                    SettingsButtonManager.Instance.SetSfx(isOn);
                    break;
                case SettingType.Vibration:
                    SettingsButtonManager.Instance.SetVibration(isOn);
                    break;
            }

            if (settingType == SettingType.Vibration && isOn)
                VibrationHelper.Vibrate(100);
        }
    }
}