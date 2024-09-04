using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]private SkyboxObject[] skyboxObjects;
    [SerializeField] private Image displaySkyboxImage;
    [SerializeField] private Light directionalLight;
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    private int skyBoxIndex;
    private bool shadowsOn = true;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
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
    
    
}
