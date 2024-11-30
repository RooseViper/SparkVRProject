using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class GlassBox : MonoBehaviour
    {
        [SerializeField]
        private XRGrabInteractable dragonPiece;

        private readonly string layerToAdd = "DragonPiece";
        public void MakePieceInteractable()
        {
            // Get the interaction layer index by name
            var layerIndex = LayerMask.NameToLayer(layerToAdd);

            if (layerIndex == -1)
            {
                Debug.LogWarning($"Layer '{layerToAdd}' does not exist!");
                return;
            }

            // Create a new InteractionLayerMask with the desired layer added
            var newLayerMask = dragonPiece.interactionLayers | (1 << layerIndex);

            // Assign the new mask back to the interactionLayers property
            dragonPiece.interactionLayers = newLayerMask;
            Debug.Log("Picked");
        }
    }
}
