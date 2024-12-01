using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape_Room
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject chestDoorObject;
        [SerializeField] private ParticleSystem sparksPs;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Open()
        {
            LeanTween.rotateAroundLocal(chestDoorObject, Vector3.left, 95f, 2f).setEaseInOutSine();
            Escape_Room.Audio.AudioManager.Instance.Play("Chest Open", audioSource);
            Sparks();
        }

        private void Sparks()
        {
            StartCoroutine(SparksCorutine());
        }

        private IEnumerator SparksCorutine()
        {
            yield return new WaitForSeconds(1f);
            sparksPs.Play();
            var audioManager = Escape_Room.Audio.AudioManager.Instance;
            audioManager.Play("Celebration");
            audioManager.Play("Fireworks");
            yield return new WaitForSeconds(1.55f);
            sparksPs.gameObject.SetActive(false);
        }
    }
}
