using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioClip successAudioClip, failAudioClip;
    private AudioSource audioSource;
    public static AudioManager instance=> _instance;
    private static AudioManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySuccessAudioClip() => audioSource.PlayOneShot(successAudioClip);
    public void PlayFailAudioClip() => audioSource.PlayOneShot(failAudioClip);
    public void PlayAudioSourceDelayed(AudioSource source) => source.PlayDelayed(1.5f);

}
