using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip clip;
        [Range(0F,1F)]
        public float volume = 1f; //The rate at which the Audio Source gradually decreases
        [HideInInspector]
        public AudioSource audioSource;
    }
}
