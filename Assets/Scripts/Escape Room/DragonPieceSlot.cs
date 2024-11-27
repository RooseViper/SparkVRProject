using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Escape_Room
{
    public class DragonPieceSlot : MonoBehaviour
    {
        [SerializeField] private int index;
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
                
            }
        }

        private void RemoveDragonPiece(SelectExitEventArgs selectExitEventArgs)
        {
            dragonPiece = null;
        }
    }
}
