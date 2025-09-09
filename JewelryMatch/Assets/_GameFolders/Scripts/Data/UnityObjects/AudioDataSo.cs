using UnityEngine;

namespace _GameFolders.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 0)]
    public class AudioDataSo : ScriptableObject
    {
        [Header("Music")] 
        [SerializeField] private AudioClip gameMusic;
        
        [Header("SFX")]
        [SerializeField] private AudioClip buttonClickSfx;
        [SerializeField] private AudioClip winSfx;
        [SerializeField] private AudioClip slotsFullSfx;
        [SerializeField] private AudioClip timeOutSfx;
        [SerializeField] private AudioClip collectItemSfx;
        [SerializeField] private AudioClip selectItemSfx;
        [SerializeField] private AudioClip matchSoundSfx;
        
        public AudioClip GameMusic => gameMusic;
        public AudioClip ButtonClickSfx => buttonClickSfx;
        public AudioClip WinSfx => winSfx;
        public AudioClip SlotsFullSfx => slotsFullSfx;
        public AudioClip CollectItemSfx => collectItemSfx;
        public AudioClip SelectItemSfx => selectItemSfx;
        public AudioClip TimeOutSfx => timeOutSfx;
        public AudioClip SlotFullSfx => slotsFullSfx;
        public AudioClip MatchSoundSfx => matchSoundSfx;
    }
}