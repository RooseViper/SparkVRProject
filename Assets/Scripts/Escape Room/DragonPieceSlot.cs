using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class DragonPieceSlot : MonoBehaviour
    {
        public int index;
        [SerializeField] private XRGrabInteractable startingPieceInteractable;
        private Item item;
        private bool HasPiece => xrSocketInteractor.hasSelection;
        private XRSocketInteractor xrSocketInteractor;
        private AudioSource doorAudioSource;
        // Start is called before the first frame update
        private void Start()
        {
            xrSocketInteractor = GetComponent<XRSocketInteractor>();
            doorAudioSource = GetComponentInParent<AudioSource>();
            if (startingPieceInteractable != null)
            {
                StartCoroutine(InitializeCoruotine());
            }
            else
            {
                AddListeners();
            }
        }

        private IEnumerator InitializeCoruotine()
        {
            yield return new WaitForEndOfFrame();
            IXRSelectInteractable interactable = startingPieceInteractable;
            AddListeners();
            xrSocketInteractor.StartManualInteraction(interactable);
        }

        private void AddListeners()
        {
            xrSocketInteractor.selectEntered.AddListener(SetDragonPiece);
            xrSocketInteractor.selectExited.AddListener(RemoveDragonPiece);
        }
        
        private void SetDragonPiece(SelectEnterEventArgs selectEnterEventArgs)
        {
            var attachedDragonPiece = xrSocketInteractor.firstInteractableSelected.transform.GetComponent<Item>();
            if (attachedDragonPiece != null)
            {
                item = attachedDragonPiece;
                item.MakeKinematic();
            }
            var dragonPieceSlots = transform.parent.GetComponentsInChildren<DragonPieceSlot>();
            var allSlotsFilled = dragonPieceSlots.All(slot => slot.HasPiece);
            if (allSlotsFilled)
            {
                var allPiecesMatched = dragonPieceSlots.All(slot => slot.index == slot.item.index);
                if (allPiecesMatched)
                {
                    StartCoroutine(OpenDoorCoroutine());
                }
            }
        }

        private IEnumerator OpenDoorCoroutine()
        {
            yield return new WaitForSeconds(1f);
            Escape_Room.Audio.AudioManager.Instance.Play("Door Unlock", doorAudioSource);
            yield return new WaitForSeconds(1f);
            var door = GetComponentInParent<Door>();
            door.Open();
            Escape_Room.Audio.AudioManager.Instance.Play("Creaky Door", doorAudioSource);
        }

        private void RemoveDragonPiece(SelectExitEventArgs selectExitEventArgs)
        {
            item.MakeUnKinematic();
            item = null;
        }
    }
}
