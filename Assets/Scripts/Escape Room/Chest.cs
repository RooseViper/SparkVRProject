using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escape_Room
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject chestDoorObject;
        [SerializeField] private ParticleSystem sparksPs;
        

        public void Open()
        {
            LeanTween.rotateAroundLocal(chestDoorObject, Vector3.left, 95f, 2f).setEaseInOutSine();
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
            yield return new WaitForSeconds(1f);
            sparksPs.gameObject.SetActive(false);
        }
    }
}
