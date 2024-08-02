using System;
using Common;
using Common.Attributes;
using UnityEngine;

namespace Managers
{
    [InitializeSingleton]
    public class AudioManager : MonoSingleton<AudioManager>
    {
        /// <summary>
        /// Special singleton initializer method.
        /// </summary>
        public new static void Initialize()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Managers/AudioManager");
            if (prefab == null) throw new Exception("Missing AudioManager prefab!");

            var instance = Instantiate(prefab);
            if (instance == null) throw new Exception("Failed to instantiate AudioManager prefab!");

            instance.name = "Managers.AudioManager (MonoSingleton)";
        }

        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private void Start()
        {
            PlaySound("MainMenuTheme");
            PlaySFX("Test");
        }

        private void PlaySound(string sound)
        {
            var s = Array.Find(musicSounds, x => x.name == sound);
            if (s == null) return;

            musicSource.clip = s.clip;
            musicSource.Play();
        }

        private void PlaySFX(string sound)
        {
            var s = Array.Find(sfxSounds, x => x.name == sound);
            if (s == null) return;

            sfxSource.PlayOneShot(s.clip);
        }

        public void ToggleMusic()
            => musicSource.mute = !musicSource.mute;

        public void ToggleSFX()
            => sfxSource.mute = !sfxSource.mute;

        public void MusicVolume(float volume)
            => musicSource.volume = volume;

        public void SFXVolume(float volume)
            => sfxSource.volume = volume;
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }
}
