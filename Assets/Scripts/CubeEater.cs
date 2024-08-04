using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEater : MonoBehaviour
{
    public Cube.CubeColor cubeColor;
    [SerializeField]private CountdownTimer countdownTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        var cube = coll.GetComponent<Cube>();
        if (cube != null)
        {
            if (cube.cubeColor == cubeColor)
            {
                Destroy(cube.gameObject);
                AudioManager.instance.PlaySuccessAudioClip();
            }
            else
            {
                Destroy(cube.gameObject);
                countdownTimer.DeductTime();
                AudioManager.instance.PlayFailAudioClip();
            }
        }
    }
}
