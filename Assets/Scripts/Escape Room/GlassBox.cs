using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class GlassBox : MonoBehaviour
    {
        [SerializeField]
        private XRGrabInteractable piece;
        [SerializeField] private GameObject triggerObject;

        public void MakePieceInteractable()
        {
            // Create a new InteractionLayerMask with the desired layer added
            var newLayerMask = piece.interactionLayers | (1 << 30);

            // Assign the new mask back to the interactionLayers property
            piece.interactionLayers = newLayerMask;
            triggerObject.SetActive(true);
        }
    }
}
