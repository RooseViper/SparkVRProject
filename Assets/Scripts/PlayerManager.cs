using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public Transform playerRig;
   [SerializeField] private Transform teleportTransform;
   
   [SerializeField] private Transform blackRoomTransform;
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

   public void TeleportToBlackRoom()
   {
      playerRig.SetLocalPositionAndRotation(blackRoomTransform.position, Quaternion.Euler(blackRoomTransform.eulerAngles));
   }

   private IEnumerator WelcomeVoiceCoroutine()
   {
      yield return new WaitForSeconds(0.75f);
      Escape_Room.Audio.AudioManager.Instance.Play("Welcome");
      yield return new WaitForSeconds(7f);
      GameManager.Instance.StartTimer();
   }
}
