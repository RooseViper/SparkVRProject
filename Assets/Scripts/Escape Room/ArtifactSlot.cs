using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class ArtifactSlot : MonoBehaviour
    {
        public int index;
        private bool HasPiece => xrSocketInteractor.hasSelection;
        private XRSocketInteractor xrSocketInteractor;
        private Item item;
        private GlassBox glassBox;
        // Start is called before the first frame update
        private void Start()
        {
            xrSocketInteractor = GetComponent<XRSocketInteractor>();
            glassBox = GetComponentInParent<GlassBox>();
            xrSocketInteractor.selectEntered.AddListener(SetArtifactPiece);
            xrSocketInteractor.selectExited.AddListener(RemoveArtifactPiece);
        }
        
        private void SetArtifactPiece(SelectEnterEventArgs selectEnterEventArgs)
        {
            var attachedArtifact = xrSocketInteractor.firstInteractableSelected.transform.GetComponent<Item>();
            if (attachedArtifact != null)
            {
                item = attachedArtifact;
                item.MakeKinematic();
            }
            var artifactSlots = transform.parent.GetComponentsInChildren<ArtifactSlot>();
            var allSlotsFilled = artifactSlots.All(slot => slot.HasPiece);
            if (allSlotsFilled)
            {
                var allPiecesMatched = artifactSlots.All(slot => slot.index == slot.item.index);
                if (allPiecesMatched)
                {
                    var door = transform.parent.GetComponentInChildren<Door>();
                    door.Open();
                    glassBox.MakePieceInteractable();
                }
            }
        }

        private void RemoveArtifactPiece(SelectExitEventArgs selectExitEventArgs)
        {
            item.MakeUnKinematic();
            item = null;
        }
    }
}
