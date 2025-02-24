using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    public void Shrink()=>LeanTween.scale(gameObject, Vector3.zero, 0.25f).setEaseInOutSine().setOnComplete(OnEnd);

    private void OnEnd() => canvas.enabled = false;
}
