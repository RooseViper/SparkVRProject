using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGetter : MonoBehaviour
{
    public void EnableJoyStick(GameObject obj)
    {
        var handMesh = obj.GetComponentInChildren<HandMesh>();
        handMesh.EnableDisableMesh(true);
    }
    
    public void DisableJoyStick(GameObject obj)
    {
        var handMesh = obj.GetComponentInChildren<HandMesh>();
        handMesh.EnableDisableMesh(false);
    }
}
