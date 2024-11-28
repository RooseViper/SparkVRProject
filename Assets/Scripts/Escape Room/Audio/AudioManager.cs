using System;
using Audio;
using UnityEngine;

namespace Escape_Room.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public GameAudioSource[] soundFXAudioSources;
        public AudioSource[] musicAudioSources;
        public static AudioManager Instance => _instance;
        private static AudioManager _instance;
        
        private void Awake()
        {
            _instance = this;
            foreach (var sound in sounds)
            {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.playOnAwake = false;
                sound.audioSource.clip = sound.clip;
                sound.audioSource.volume = sound.volume;
            }
        }

       
        /// <summary>
        /// Plays an Audio Clip
        /// </summary>
        /// <param name="name"></param>
        public void Play(string name)
        {
            var sound = Array.Find(sounds, sObj => sObj.soundName == name);
            if (sound == null)
            {
                Debug.LogWarning("Sound " + name + "not found");
            }
            else
            {
                sound.audioSource.volume = sound.volume;
                if (sound.audioSource.isPlaying)
                {
                    sound.audioSource.Stop();
                }
                sound.audioSource.Play();
            }
        }
        
        public void Stop(string name)
        {
            var sound = System.Array.Find(sounds, sObj => sObj.soundName == name);
            if (sound == null)
            {
                Debug.LogWarning("Sound " + name + "not found");
            }
            else
            {
                if(sound.audioSource.isPlaying)return;
                sound.audioSource.Stop();
            }
        }
        /// <summary>
        /// Plays an Audiosource
        /// </summary>
        /// <param name="name"></param>
        public void Play(string name, AudioSource audioSource)
        {
            var sound = System.Array.Find(sounds, sObj => sObj.soundName == name);
            if (sound == null)
            {
                Debug.LogWarning("Sound " + name + "not found");
            }
            else
            {
                audioSource.volume = sound.volume;
                audioSource.Stop();
                audioSource.PlayOneShot(sound.clip);
            }
        }

    }
}
