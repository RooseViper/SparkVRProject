using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Micosmo.SensorToolkit;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootSteps : MonoBehaviour
{
   [SerializeField]private AudioClip[] concreteFootStepClips;
   [SerializeField]private AudioClip[] metalFootStepClips;
   [SerializeField]private AudioClip[] woodenFootStepClips;
   [SerializeField]private AudioClip[] movementClips;
   [SerializeField]private AudioSource audioSource;
   [SerializeField]private LOSSensor floorSensor; //Supported with a Range sensor
   private GameObject detectedFloor;

   /// <summary>
   /// Called in an animaton between keyframes
   /// </summary>
    public void PlayFootStep()
    {
        detectedFloor = floorSensor.GetNearestDetection();
        if(detectedFloor == null)return;
        if (detectedFloor.CompareTag("Concrete") || detectedFloor.CompareTag("Untagged"))
        {
            audioSource.PlayOneShot(concreteFootStepClips[Random.Range(0, concreteFootStepClips.Length)]);
        }  
        else if (detectedFloor.CompareTag("Metal"))
        {
            audioSource.PlayOneShot(metalFootStepClips[Random.Range(0, metalFootStepClips.Length)]);
        }
        else if (detectedFloor.CompareTag("Wood"))
        {
            audioSource.PlayOneShot(woodenFootStepClips[Random.Range(0, woodenFootStepClips.Length)]);
        }
    }

   public void PlayMovementClip()=> audioSource.PlayOneShot(woodenFootStepClips[Random.Range(0, woodenFootStepClips.Length)]);

   public void TriggerChase()
   {
       var oldManAi = GetComponentInParent<OldManAi>();
       if(oldManAi == null)return;
       oldManAi.Chase();
   }
}
