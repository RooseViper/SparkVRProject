using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class GlassBox : MonoBehaviour
    {
        [SerializeField]
        private XRGrabInteractable dragonPiece;


        public void MakePieceInteractable()
        {
            // Create a new InteractionLayerMask with the desired layer added
            var newLayerMask = dragonPiece.interactionLayers | (1 << 30);

            // Assign the new mask back to the interactionLayers property
            dragonPiece.interactionLayers = newLayerMask;
        }
    }
}
