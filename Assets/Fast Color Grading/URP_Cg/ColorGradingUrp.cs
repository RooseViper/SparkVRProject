namespace UnityEngine.Rendering.Universal
{
    public class ColorGradingUrp : ScriptableRendererFeature
    {
        public static ColorGradingUrp Instance { get; set; }
        [System.Serializable]
        public class ColorGradingSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
            public Material Material;
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
            public Texture2D BlurMask;
            public Color VignetteColor = Color.black;
            [Range(0, 1)]
            public float VignetteAmount = 0f;
            [Range(0.001f, 1)]
            public float VignetteSoftness = 0.0001f;
        }

        public ColorGradingSettings settings = new ColorGradingSettings();

        ColorGradingUrpPass colorGradingUrpPass;

        public override void Create()
        {
            colorGradingUrpPass = new ColorGradingUrpPass(settings.Event, settings.Material, 
                settings.Color, settings.Hue, settings.Contrast, settings.Brightness, settings.Saturation, settings.Exposure, settings.Gamma, 
                settings.Sharpness, settings.Blur, settings.BlurMask, 
                settings.VignetteColor, settings.VignetteAmount, settings.VignetteSoftness, this.name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (Instance == null)
            {
                Instance = this;
            };
            colorGradingUrpPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(colorGradingUrpPass);
        }
    }
}

