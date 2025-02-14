using System.Collections;
using System.Collections.Generic;
using Content.Glo;
using Micosmo.SensorToolkit;
using UnityEngine;

public class SightSensor : MonoBehaviour
{
    private RaySensor raySensor;
    // Start is called before the first frame update
    private void Start()
    {
        raySensor = GetComponent<RaySensor>();
        raySensor.OnDetected.AddListener(OnDetected);
        raySensor.OnLostDetection.AddListener(OnLostDetection);
    }

    private void OnDetected(GameObject detectedObject, Sensor sensor)
    {
        if (detectedObject.CompareTag("DisplayScreen"))
        {
            var tableContent = detectedObject.GetComponent<TableContent>();
            if (tableContent != null)
            {
                tableContent.PlayStopAudio(true);
                Debug.Log("Show Audio");
            }
        }
    }
    
    private void OnLostDetection(GameObject detectedObject, Sensor sensor)
    {
        if (detectedObject.CompareTag("DisplayScreen"))
        {
            var tableContent = detectedObject.GetComponent<TableContent>();
            if (tableContent != null)
            {
                tableContent.PlayStopAudio(false);
                Debug.Log("Stop Audio");
            }
        }
    }
    
}
