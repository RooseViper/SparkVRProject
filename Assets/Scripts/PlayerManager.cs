using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public Transform playerRig;
   [SerializeField] private Transform teleportTransformLevel, teleportTransformStartScene;
   public static PlayerManager Instance => _instance;
   private static PlayerManager _instance;

   private void Awake()
   {
      _instance = this;
   }

   private void Start()
   {
      StartCoroutine(SnapPlayerCoroutine());
   }

   /// <summary>
   /// Snaps Player to starting room
   /// </summary>
   /// <returns></returns>
   private IEnumerator SnapPlayerCoroutine()
   {
      yield return new WaitForEndOfFrame();
      playerRig.SetLocalPositionAndRotation(teleportTransformStartScene.position, Quaternion.Euler(teleportTransformStartScene.eulerAngles));
   }

   public void Teleport()
   {
      playerRig.SetLocalPositionAndRotation(teleportTransformLevel.position, Quaternion.Euler(teleportTransformLevel.eulerAngles));
   }
}
