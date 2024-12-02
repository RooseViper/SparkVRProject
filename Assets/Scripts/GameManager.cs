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
    [SerializeField ]private float fogRate = 0.05f;
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
        ftLightmaps.RefreshFull();
    }
    
    public void ChangeShadowState()
    {
        shadowsOn = !shadowsOn;
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
    public void IncreaseFog()=>   StartCoroutine(IncreaseFogDensity(1f, 12.5f));
    private IEnumerator IncreaseFogDensity(float target, float time)
    {
        var startDensity = RenderSettings.fogDensity; // Current fog density
        var elapsed = 0f;

        // Gradually increase the fog density
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, target, elapsed / time);
            yield return null; // Wait for the next frame
        }
        // Ensure the final density is exactly the target
        RenderSettings.fogDensity = target;
        RestartExperience();
    }

    public void QuitExperience()=> Application.Quit();

    public void RestartExperience() => SceneManager.LoadScene(0);

}
