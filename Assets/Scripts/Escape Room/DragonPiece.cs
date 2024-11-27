using UnityEngine;

namespace Escape_Room
{
    public class DragonPiece : MonoBehaviour
    {
        public void PrintDragonPiece() => Debug.Log("Print Information" + transform.parent.gameObject.name);
    }
}
