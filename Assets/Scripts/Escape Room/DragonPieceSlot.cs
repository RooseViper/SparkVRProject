using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class DragonPieceSlot : MonoBehaviour
    {
        public int index;
        public DragonPiece DragonPiece => dragonPiece;
        private DragonPiece dragonPiece;
        private XRSocketInteractor xrSocketInteractor;
        // Start is called before the first frame update
        private void Start()
        {
            xrSocketInteractor = GetComponent<XRSocketInteractor>();
            xrSocketInteractor.selectEntered.AddListener(SetDragonPiece);
            xrSocketInteractor.selectExited.AddListener(RemoveDragonPiece);
        }

        private void SetDragonPiece(SelectEnterEventArgs selectEnterEventArgs)
        {
            var attachedDragonPiece = xrSocketInteractor.firstInteractableSelected.transform.GetComponent<DragonPiece>();
            if (attachedDragonPiece != null)
            {
                dragonPiece = attachedDragonPiece;
            }
            var dragonSlots = transform.parent.GetComponentsInChildren<DragonPieceSlot>();
            var allPiecesMatched = dragonSlots.All(slot => slot.index == slot.dragonPiece.index);
            if (allPiecesMatched)
            {
                Debug.Log("Open Door");
            }
        }

        private void RemoveDragonPiece(SelectExitEventArgs selectExitEventArgs)
        {
            dragonPiece = null;
        }
    }
}
