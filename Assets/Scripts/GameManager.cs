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

    public enum Theme
    {
        Political,
        Tourism
    }
    public Theme theme;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private int skyBoxIndex;
    private bool shadowsOn = true;
    private Vector3 defaultInstructionsCanvasSize, defaultInstructionsVideoCanvasSize;
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
        intructionsVideoPlayer = videosInstructionsCanvas.GetComponentInChildren<VideoPlayer>();
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

    public void ChangeInstructionsCanvasState(bool expand)
    {
        if (LeanTween.isTweening(instructionsCanvas.gameObject))
        {
            LeanTween.cancel(instructionsCanvas.gameObject);
        }
        if (expand)
        {
            LeanTween.scale(instructionsCanvas.gameObject, defaultInstructionsCanvasSize, 0.5f).setEaseInOutSine();
        }
        else
        {
            LeanTween.scale(instructionsCanvas.gameObject, Vector3.zero, 0.5f).setEaseInOutSine();
        }
    }
    
    public void ChangeInstructionsVideosCanvasState(bool expand)
    {
        if (LeanTween.isTweening(videosInstructionsCanvas.gameObject))
        {
            LeanTween.cancel(videosInstructionsCanvas.gameObject);
        }
        if (expand)
        {
            intructionsVideoPlayer.Stop();
            intructionsVideoPlayer.Play();
            LeanTween.scale(videosInstructionsCanvas.gameObject, defaultInstructionsVideoCanvasSize, 0.5f).setEaseInOutSine();
        }
        else
        {
            LeanTween.scale(videosInstructionsCanvas.gameObject, Vector3.zero, 0.5f).setEaseInOutSine();
        }
    }

    public void QuitExperience()
    {
        Application.Quit();
    }

}
