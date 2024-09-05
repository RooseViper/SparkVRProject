using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMesh : MonoBehaviour
{
    [SerializeField] private GameObject handObject;
    private MeshRenderer meshRenderer, highlightedButtonMesh;
    private Coroutine toggleObjectCoroutine;
    // Start is called before the first frame update
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        if (toggleObjectCoroutine != null)
        {
            StopCoroutine(toggleObjectCoroutine);
        }
        highlightedButtonMesh = handObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        highlightedButtonMesh.enabled = true;
        toggleObjectCoroutine = StartCoroutine(ToggleObjectCoroutine());
    }

    private void OnDisable()
    {
        if (toggleObjectCoroutine != null)
        {
            StopCoroutine(toggleObjectCoroutine);
        }
    }

    private IEnumerator ToggleObjectCoroutine()
    {
        while (true)
        {
            highlightedButtonMesh.enabled = !highlightedButtonMesh.enabled;
                // Wait for the specified interval before continuing
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void EnableDisableMesh(bool enable)
    {
        meshRenderer.enabled = !enable;
        handObject.SetActive(enable);
    }

}
