using UnityEngine;

namespace Escape_Room
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float openAngle;
        public void Open()
        {
            LeanTween.rotateLocal(gameObject, new Vector3(0f, openAngle, 0f), 5f).setEaseInOutSine();
        }
    }
}
