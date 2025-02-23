using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    [SerializeField] private int numberIndex;
    private KeyPad keyPad;
    // Start is called before the first frame update
    private void Start()
    {
        keyPad = GetComponentInParent<KeyPad>();
    }
    public void Interact()
    {
        Escape_Room.Audio.AudioManager.Instance.Play("Button Press Keypad");
        Escape_Room.Audio.AudioManager.Instance.Play("Keypad Beep");
        LeanTween.moveLocalZ(gameObject, 4.7525f, 0.15f).setEaseInOutSine().setOnComplete(ReturnToPosition);
        keyPad.EnterCode(numberIndex);
        gameObject.layer = 0;
    }

    private void ReturnToPosition() => LeanTween.moveLocalZ(gameObject, 4.744552f, 0.15f).setEaseInOutSine();
}