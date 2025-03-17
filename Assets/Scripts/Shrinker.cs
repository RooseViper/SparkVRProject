using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    private Canvas canvas;
    private Vector3 defaultScale;
    [SerializeField]private bool shrink;
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        var canvasTransform = canvas.transform;
        defaultScale = canvasTransform.localScale;
        canvasTransform.localScale = Vector3.zero;
        if (!shrink)
        {
            UnShrink();
        }
    }

    // Start is called before the first frame update
    public void Shrink()=>LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(OnEnd);

    public void UnShrink()
    {
        canvas.enabled = true;
        LeanTween.scale(gameObject, defaultScale, 0.5f).setEaseInOutSine();
    }

    private void OnEnd() => canvas.enabled = false;
}
