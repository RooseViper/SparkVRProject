using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class DragonPiece : MonoBehaviour
    {
        public int index;
        private Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void MakeKinematic() => rb.isKinematic = true;
        public void MakeUnKinematic() => rb.isKinematic = false;
    }
}
