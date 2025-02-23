using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text keypadText;
    private GameObject hiddenDoor; 
    private string codeTyped = "";
    private Coroutine clearKeypadCoroutine;
    public string SecretCode { get; set; }

    private void Start()
    {
        hiddenDoor = transform.parent.gameObject;
    }

    public void EnterCode(int numberIndex)
    {
        codeTyped += numberIndex;
        DisplayCode();
        CheckCode();
    }
        
    /// <summary>
    /// Customizes the length based on length and changes it by adding things like dashes or changing the color.
    /// </summary>
    private void DisplayCode()
    {
        // Determine how many dashes to add based on the length of the string
        var dashesToAdd = Mathf.Max(0, 4 - codeTyped.Length);
        // Generate the dash string
        var dashes = new string('-', dashesToAdd);
        if (codeTyped.Length <= 4)
        {
            ColorUtility.TryParseHtmlString("#32C000", out var greenColor);
            keypadText.color = greenColor;
        }
        keypadText.text = dashes + codeTyped;
    }

    private void CheckCode()
    {
        if (codeTyped.Length <= 3) return;
        if (string.IsNullOrEmpty(SecretCode) || (SecretCode != codeTyped))
        {
            ClearCode();
        }
        else if (SecretCode == codeTyped)
        {
            LeanTween.moveLocalY(hiddenDoor, 12.01f, 2.975f).setEaseInOutSine();
            Escape_Room.Audio.AudioManager.Instance.Play("Hidden Wall");
        }
    }


    private void ClearCode()
    {
        ColorUtility.TryParseHtmlString("#FF1B00", out var redColor);
        Escape_Room.Audio.AudioManager.Instance.Play("Keypad Beep Negative");
        keypadText.color = redColor;
        keypadText.text = "----";
        codeTyped = "";
    }

}