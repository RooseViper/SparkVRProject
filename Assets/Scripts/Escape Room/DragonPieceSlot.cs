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
        public DragonPiece dragonPiece;
        private bool HasPiece => xrSocketInteractor.hasSelection;
        private XRSocketInteractor xrSocketInteractor;
        // Start is called before the first frame update
        private void Start()
        {
            xrSocketInteractor = GetComponent<XRSocketInteractor>();
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
            var attachedDragonPiece = xrSocketInteractor.firstInteractableSelected.transform.GetComponent<DragonPiece>();
            if (attachedDragonPiece != null)
            {
                dragonPiece = attachedDragonPiece;
                dragonPiece.MakeKinematic();
            }
            var dragonPieceSlots = transform.parent.GetComponentsInChildren<DragonPieceSlot>();
            var allSlotsFilled = dragonPieceSlots.All(slot => slot.HasPiece);
            if (allSlotsFilled)
            {
                Debug.Log("All Slots filled");
                var allPiecesMatched = dragonPieceSlots.All(slot => slot.index == slot.dragonPiece.index);
                if (allPiecesMatched)
                {
                    var door = GetComponentInParent<Door>();
                    door.Open();
                }
            }
        }

        private void RemoveDragonPiece(SelectExitEventArgs selectExitEventArgs)
        {
            dragonPiece.MakeUnKinematic();
            dragonPiece = null;
        }
    }
}
