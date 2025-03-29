using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]private float target;
    [SerializeField] private OldManAi oldManAi;
    private AudioSource audioSource;
    private bool opened;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Open()
    {
        if(opened)return;
        Escape_Room.Audio.AudioManager.Instance.Play("Open Gate", audioSource);
        LeanTween.moveLocalY(gameObject, target, 2f).setEaseInOutSine().setOnComplete(OnReachedEnd);
        opened = true;
    }

    private void OnReachedEnd()
    {
        Escape_Room.Audio.AudioManager.Instance.Play("Open Gate End", audioSource);
        oldManAi.LockDown();
    }
}
