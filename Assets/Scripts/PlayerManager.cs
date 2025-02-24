using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public Transform playerRig;
   [SerializeField] private Transform teleportTransform;
   [SerializeField]private Transform teleportBlackRoomTransform;
   [SerializeField] private GameObject triggerObjectSuccess, triggerObjectFailure;
   [SerializeField] private AudioSource theEndAudioSource;
   public static PlayerManager Instance => _instance;
   private static PlayerManager _instance;

   private void Awake()
   {
      _instance = this;
   }
   public void Teleport()
   {
      playerRig.SetLocalPositionAndRotation(teleportTransform.position, Quaternion.Euler(teleportTransform.eulerAngles));
      StartCoroutine(WelcomeVoiceCoroutine());
   }

   public void EndgameSucess()
   {
      playerRig.SetLocalPositionAndRotation(teleportBlackRoomTransform.position, Quaternion.Euler(teleportBlackRoomTransform.eulerAngles));
      triggerObjectSuccess.SetActive(true);
      theEndAudioSource.Play();
      Escape_Room.Audio.AudioManager.Instance.Play("The End");
   }

   private IEnumerator WelcomeVoiceCoroutine()
   {
      yield return new WaitForSeconds(0.75f);
      Escape_Room.Audio.AudioManager.Instance.Play("Welcome");
   }
}
