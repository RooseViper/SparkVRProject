using System;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField]
        private Transform targetTransform;

        public void StartExperience(HVRTeleporter hvrTeleporter)
        {
            hvrTeleporter.Teleport(targetTransform.position, Vector3.forward);
        }
    }
}
