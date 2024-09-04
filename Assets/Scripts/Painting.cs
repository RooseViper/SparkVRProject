using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    [SerializeField] private Transform textShown;
    [SerializeField] private Button expandButton;
    private Vector3 defaultTextSize;
    private bool isExpanded;
    // Start is called before the first frame update
    private void Start()
    {
        defaultTextSize = textShown.localScale;
        expandButton.onClick.AddListener(ChangeTextSize);
        textShown.localScale = Vector3.zero;
    }
    
    private void ChangeTextSize()
    {
        isExpanded =!isExpanded;
        if (isExpanded)
        {
            LeanTween.scale(textShown.gameObject, defaultTextSize, 0.5f).setEaseInOutSine().setOnComplete(ReachedEnd);
        }
        else
        {
            LeanTween.scale(textShown.gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(ReachedEnd);
        }
        expandButton.interactable = false;
    }
    private void ReachedEnd() => expandButton.interactable = true;
}
