using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField]private SkyboxObject[] skyboxObjects;
    [SerializeField] private Transform instructionsCanvas, videosInstructionsCanvas, portableMenuCanvas;
    [SerializeField] private Image[] displaySkyboxImages;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private int skyBoxIndex;
    private bool shadowsOn = true;
    private Vector3 defaultInstructionsCanvasSize, defaultInstructionsVideoCanvasSize, defaultPortableMenuCanvasSize;
    private VideoPlayer intructionsVideoPlayer;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
       defaultInstructionsCanvasSize = instructionsCanvas.localScale;
   //   defaultInstructionsVideoCanvasSize = videosInstructionsCanvas.localScale;
       defaultPortableMenuCanvasSize = portableMenuCanvas.localScale;
       instructionsCanvas.localScale = Vector3.zero;
   //    videosInstructionsCanvas.localScale = Vector3.zero;
       portableMenuCanvas.localScale = Vector3.zero;
       intructionsVideoPlayer = videosInstructionsCanvas.GetComponentInChildren<VideoPlayer>();
       StartCoroutine(OpenInstructionsCoroutine());
    }

    private IEnumerator OpenInstructionsCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ChangeInstructionsCanvasState(true);
    }

    public void ChangeSkyBox()
    {
        skyBoxIndex++;
        if (skyBoxIndex > (skyboxObjects.Length - 1))
        {
            skyBoxIndex = 0;
        }
        displaySkyboxImages.ToList().ForEach(image=> image.sprite = skyboxObjects[skyBoxIndex].sprite);
        RenderSettings.skybox = skyboxObjects[skyBoxIndex].material;
        RenderSettings.ambientSkyColor = skyboxObjects[skyBoxIndex].color;
    }

    public void ChangeShadowState()
    {
        shadowsOn = !shadowsOn;
        var pointLights = FindObjectsOfType<Light>().ToList();
        pointLights.ForEach(l=> l.shadows = shadowsOn ? LightShadows.Soft : LightShadows.None);
    }
    public void EnableDisableFPSCounter(TextMeshProUGUI textMeshProUGUI)=>textMeshProUGUI.gameObject.SetActive(!textMeshProUGUI.gameObject.activeInHierarchy);

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
    public void ChangePortableCanvasCanvasState(bool expand)
    {
        if (LeanTween.isTweening(portableMenuCanvas.gameObject))
        {
            LeanTween.cancel(portableMenuCanvas.gameObject);
        }
        if (expand)
        {
            LeanTween.scale(portableMenuCanvas.gameObject, defaultPortableMenuCanvasSize, 0.25f).setEaseInOutSine();
        }
        else
        {
            LeanTween.scale(portableMenuCanvas.gameObject, Vector3.zero, 0.5f).setEaseInOutSine();
        }
    }

    public void QuitExperience()=> Application.Quit();

    public void RestartExperience() => SceneManager.LoadScene(0);

}
