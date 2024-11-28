using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class AudioSourcePlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clips;

        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void Play() => _audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);

        public void Stop() => _audioSource.Stop();
    }
}
