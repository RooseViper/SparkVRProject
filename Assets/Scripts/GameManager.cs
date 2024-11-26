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
    [SerializeField] private Transform portableMenuCanvas;
    [SerializeField] private Light directionalLight;

    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private int skyBoxIndex;
    private bool shadowsOn = true;
    private Vector3 defaultPortableMenuCanvasSize;
    private VideoPlayer intructionsVideoPlayer;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        defaultPortableMenuCanvasSize = portableMenuCanvas.localScale;
        portableMenuCanvas.localScale = Vector3.zero;
    }
    
    public void ChangeShadowState()
    {
        shadowsOn = !shadowsOn;
        directionalLight.shadows = shadowsOn ? LightShadows.Soft : LightShadows.None;
    }
    public void EnableDisableFPSCounter(TextMeshProUGUI textMeshProUGUI)=>textMeshProUGUI.gameObject.SetActive(!textMeshProUGUI.gameObject.activeInHierarchy);

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
