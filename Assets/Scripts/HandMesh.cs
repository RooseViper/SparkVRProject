using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMesh : MonoBehaviour
{
    [SerializeField] private GameObject handObject;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void EnableDisableMesh(bool enable)
    {
        meshRenderer.enabled = !enable;
        handObject.SetActive(enable);
    }

}
