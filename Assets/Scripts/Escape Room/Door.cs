using System;
using UnityEngine;

namespace Escape_Room
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float openAngle;
        [SerializeField] private float speed;

        public void Open()
        {
            var currentVector3 = transform.localEulerAngles;
            currentVector3.y = openAngle;
            LeanTween.rotateLocal(gameObject, currentVector3, speed).setEaseInOutSine();
        }
    }
}
