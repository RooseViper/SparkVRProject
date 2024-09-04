using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField]private SkyboxObject[] skyboxObjects;
    [SerializeField] private Transform instructionsCanvas, videosInstructionsCanvas;
    [SerializeField] private Image displaySkyboxImage;
    [SerializeField] private Light directionalLight;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private int skyBoxIndex;
    private bool shadowsOn = true;
    private Vector3 defaultInstructionsCanvasSize, defaultInstructionsVideoCanvasSize;
    private bool isExpandedInstructions, isExpandedInstructionsVideo;
    private bool isExpandingInstructions, isExpandingInstructionsVideo;
    private VideoPlayer intructionsVideoPlayer;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        defaultInstructionsCanvasSize = instructionsCanvas.localScale;
        defaultInstructionsVideoCanvasSize = videosInstructionsCanvas.localScale;
        instructionsCanvas.localScale = Vector3.zero;
        videosInstructionsCanvas.localScale = Vector3.zero;
    }

    public void ChangeSkyBox()
    {
        skyBoxIndex++;
        if (skyBoxIndex > (skyboxObjects.Length - 1))
        {
            skyBoxIndex = 0;
        }
        directionalLight.color = skyboxObjects[skyBoxIndex].color;
        displaySkyboxImage.sprite = skyboxObjects[skyBoxIndex].sprite;
        RenderSettings.skybox = skyboxObjects[skyBoxIndex].material;
        RenderSettings.ambientSkyColor = skyboxObjects[skyBoxIndex].color;
    }

    public void ChangeShadowState()
    {
        shadowsOn = !shadowsOn;
        directionalLight.shadows = shadowsOn ? LightShadows.Soft : LightShadows.None;
    }

    public void ChangeInstructionsCanvasState()
    {
        if(isExpandingInstructions)return;
        isExpandedInstructions = !isExpandedInstructions;
        if (isExpandedInstructions)
        {
            LeanTween.scale(instructionsCanvas.gameObject, defaultInstructionsCanvasSize, 0.5f).setEaseInOutSine().setOnComplete(ReachedEndInstructions);
        }
        else
        {
            LeanTween.scale(instructionsCanvas.gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(ReachedEndInstructions);
        }
        isExpandingInstructions = true;
    }
    
    public void ChangeInstructionsVideosCanvasState()
    {
        if(isExpandingInstructionsVideo)return;
        isExpandedInstructionsVideo = !isExpandedInstructionsVideo;
        if (isExpandedInstructionsVideo)
        {
            LeanTween.scale(videosInstructionsCanvas.gameObject, defaultInstructionsVideoCanvasSize, 0.5f).setEaseInOutSine().setOnComplete(ReachedEndInstructionsVideo);
        }
        else
        {
            LeanTween.scale(videosInstructionsCanvas.gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(ReachedEndInstructionsVideo);
        }
        isExpandingInstructionsVideo = true;
    }

    private void ReachedEndInstructions() => isExpandingInstructions = false;

    private void ReachedEndInstructionsVideo() => isExpandingInstructionsVideo = false;


}
