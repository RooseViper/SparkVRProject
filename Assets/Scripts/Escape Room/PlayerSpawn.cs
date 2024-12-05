using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField]
        private Transform xrRig;
        private ContinuousMoveProviderBase continuousMoveProviderBase;
        private Transform myTransform;

        private void Awake()
        {
            myTransform = transform;
        }

        private void Start()
        {
            continuousMoveProviderBase = xrRig.GetComponent<ContinuousMoveProviderBase>();
        }

        public void StartExperience()
        {
            xrRig.SetPositionAndRotation(myTransform.position, Quaternion.Euler(myTransform.eulerAngles));
            continuousMoveProviderBase.enabled = true;
        }
    }
}
