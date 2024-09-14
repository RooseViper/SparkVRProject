using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DisplayScreen : MonoBehaviour
{
    [SerializeField] private Canvas controlCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    private RenderTexture renderTexture;
    private RawImage rawImage;
    // Start is called before the first frame update
    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        renderTexture = (RenderTexture)rawImage.texture;
        
    }

    public void TurnOff()
    {
        rawImage.texture = ThemeManager.Instance.ActiveCitizenSprite;
        videoPlayer.Stop();
        controlCanvas.enabled = false;
    }
    
    public void TurnOn()
    {
        rawImage.texture = renderTexture;
        videoPlayer.Play();
        controlCanvas.enabled = true;
    }
}
