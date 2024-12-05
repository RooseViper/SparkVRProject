using System.Collections;
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
        private AudioSource glassBoxAudioSource, audioSource;
        // Start is called before the first frame update
        private void Start()
        {
            xrSocketInteractor = GetComponent<XRSocketInteractor>();
            glassBox = GetComponentInParent<GlassBox>();
            audioSource = GetComponent<AudioSource>();
            glassBoxAudioSource = glassBox.GetComponent<AudioSource>();
            xrSocketInteractor.selectEntered.AddListener(SetArtifactPiece);
            xrSocketInteractor.selectExited.AddListener(RemoveArtifactPiece);
        }
        private void RemoveFromSocket()
        {
            var attachedInteractable = xrSocketInteractor.firstInteractableSelected;
            if (attachedInteractable != null)
            {
                Escape_Room.Audio.AudioManager.Instance.Play("Object snap", audioSource);
                // Force the socket to release the interactable
                xrSocketInteractor.EndManualInteraction();
                xrSocketInteractor.interactionManager.SelectExit(xrSocketInteractor, attachedInteractable);

                // Optional: Adjust interactable's position or parent if needed
                var interactableTransform = attachedInteractable.transform;
                interactableTransform.SetParent(null); // Detach from socket
                interactableTransform.position += Vector3.left * 0.1f; // Move it slightly upwards
            }
            else
            {
                Debug.Log("No interactable is currently attached to the socket.");
            }
        }
        
        private void SetArtifactPiece(SelectEnterEventArgs selectEnterEventArgs)
        {
            var attachedArtifact = xrSocketInteractor.firstInteractableSelected.transform.GetComponent<Item>();
            if (attachedArtifact != null)
            {
                item = attachedArtifact;
                item.MakeKinematic();
            }

            if (index != attachedArtifact.index)
            {
                RemoveFromSocket();
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
                    Escape_Room.Audio.AudioManager.Instance.Play("Glassbox Open", glassBoxAudioSource);
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
