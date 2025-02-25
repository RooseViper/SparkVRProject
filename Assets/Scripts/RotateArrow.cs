using System.Collections;
using UnityEngine;

/// <summary>
/// Set the rotation of an object
/// </summary>
public class RotateArrow : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 30f; // Adjust the rotation speed as needed

    [SerializeField] private Vector3 vectorAxis;
    [SerializeField]private bool dontSpinOnStart;
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    // Start the rotation coroutine
    private void Start()
    {
        if(dontSpinOnStart)return;
        StartCoroutine(RotateOverTime());
    }

    public void StartSpinning() => StartCoroutine(RotateOverTime());
    
    private IEnumerator RotateOverTime()
    {
        while (true)
        {
            // Calculate rotation amount based on time
            var rotationAmount = rotationSpeed * Time.deltaTime;

            // Rotate the object around its local Y-axis
            myTransform.Rotate(vectorAxis, rotationAmount);

            yield return null; // Pause and resume in the next frame
        }
    }
}