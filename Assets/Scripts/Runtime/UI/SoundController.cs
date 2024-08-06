using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundController : MonoBehaviour
    {
        public Slider musicSlider, sfxSlider;

        private void Start()
        {
            // TODO: Load the saved volume settings.
        }

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(OnMusicVolume);
            sfxSlider.onValueChanged.AddListener(OnSFXVolume);
        }

        private void OnDisable()
        {
            musicSlider.onValueChanged.RemoveListener(OnMusicVolume);
            sfxSlider.onValueChanged.RemoveListener(OnSFXVolume);
        }

        public void ToggleMusic() 
            => AudioManager.Instance.ToggleMusic();

        public void ToggleSFX() 
            => AudioManager.Instance.ToggleSFX();

        private void OnMusicVolume(float volume)
            => AudioManager.Instance.MusicVolume(volume);

        private void OnSFXVolume(float volume)
            => AudioManager.Instance.SFXVolume(volume);
    }
}
