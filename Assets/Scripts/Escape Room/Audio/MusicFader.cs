using System.Collections;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Controls the Music in the Scene. Audiosources with music should have play on awake in editor, if you don't want the sound just set the volume to 0. All in all they
    /// should never be stopped.
    /// </summary>
    public class MusicFader : MonoBehaviour
    {
        public string musicName;
        private float currentVolume;
        [Range(0,1)]
        public float minVolume;
        [Range(0,1)]
        public float maxVolume;
        [Range(0.005F,0.5F)]
        public float decreaseVolumeRate = 0.1f;
        [Range(0.005F,0.5F)]
        public float increaseVolumeRate = 0.1f;
        [HideInInspector]
        public AudioSource audioSource;
        private float defaultIncreaseVolumeRate;
        /// <summary>
        /// This Plays a Jumpscare Sound Effect when Starting to Play Music.
        /// </summary>
        private bool jumpscareOnStart;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentVolume = audioSource.volume;
            jumpscareOnStart = true;
            defaultIncreaseVolumeRate = increaseVolumeRate;
        }

        private IEnumerator ReduceVolumeCoroutine()
        {
            while (currentVolume > minVolume)
            {
                currentVolume -= decreaseVolumeRate;
                audioSource.volume = currentVolume;
                if (currentVolume <= minVolume)
                {
                    StopAllCoroutines();
                    jumpscareOnStart = true; //Reenables Jumpscare only when Music completely stops atleast once.
                }
                yield return new WaitForSeconds(0.1f); // Adjust the delay to control the reduction speed
            }
        }
    
        private IEnumerator IncreaseVolumeCoroutine()
        {
            while (currentVolume < maxVolume)
            {
                currentVolume += increaseVolumeRate;
                audioSource.volume = currentVolume;
                if (currentVolume >= maxVolume)
                {
                    increaseVolumeRate = defaultIncreaseVolumeRate;
                    StopAllCoroutines();
                }
                yield return new WaitForSeconds(0.1f); // Adjust the delay to control the reduction speed
            }
        }

// Start the gradual reduction
        public void Stop()
        {
            StopAllCoroutines();
            audioSource.volume = 0;
            audioSource.Stop();
        }
        
        public void Fade()
        {
            StopAllCoroutines();
            StartCoroutine(ReduceVolumeCoroutine());
        }

        // Start the gradual reduction
        public void Play()
        {
            if (audioSource.volume < 0.15f)
            {
                audioSource.Play();
            }
            StopAllCoroutines();
            audioSource.volume = 0.5f;
            StartCoroutine(IncreaseVolumeCoroutine());
        }
    }
}
