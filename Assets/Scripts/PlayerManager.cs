using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public Transform playerRig;
   [SerializeField] private Transform teleportTransform;
   public static PlayerManager Instance => _instance;
   private static PlayerManager _instance;

   private void Awake()
   {
      _instance = this;
   }
   public void Teleport()
   {
      playerRig.SetLocalPositionAndRotation(teleportTransform.position, Quaternion.Euler(teleportTransform.eulerAngles));
   }
}
