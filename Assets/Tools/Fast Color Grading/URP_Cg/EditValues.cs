using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class EditValues : MonoBehaviour
{
    public Color Color = Color.white;
    [Range(-180, 180)]
    public int Hue = 0;
    [Range(0, 1)]
    public float Contrast = 0f;
    [Range(-1, 1)]
    public float Brightness = 0f;
    [Range(-1, 1)]
    public float Saturation = 0f;
    [Range(-1, 1)]
    public float Exposure = 0f;
    [Range(-1, 1)]
    public float Gamma = 0f;
    [Range(0, 1)]
    public float Sharpness = 0f;
    [Range(0, 1)]
    public float Blur = 0f;
    public Color VignetteColor = Color.black;
    [Range(0, 1)]
    public float VignetteAmount = 0f;
    [Range(0.001f, 1)]
    public float VignetteSoftness = 0.0001f;


    void Update()
    {
        if (ColorGradingUrp.Instance == null) return;
        ColorGradingUrp.Instance.settings.Color = Color;
        ColorGradingUrp.Instance.settings.Hue = Hue;
        ColorGradingUrp.Instance.settings.Contrast = Contrast;
        ColorGradingUrp.Instance.settings.Brightness = Brightness;
        ColorGradingUrp.Instance.settings.Saturation = Saturation;
        ColorGradingUrp.Instance.settings.Exposure = Exposure;
        ColorGradingUrp.Instance.settings.Gamma = Gamma;
        ColorGradingUrp.Instance.settings.Sharpness = Sharpness;
        ColorGradingUrp.Instance.settings.Blur = Blur;
        ColorGradingUrp.Instance.settings.VignetteColor = VignetteColor;
        ColorGradingUrp.Instance.settings.VignetteAmount = VignetteAmount;
        ColorGradingUrp.Instance.settings.VignetteSoftness = VignetteSoftness;
        ColorGradingUrp.Instance.Create();
    }
}
