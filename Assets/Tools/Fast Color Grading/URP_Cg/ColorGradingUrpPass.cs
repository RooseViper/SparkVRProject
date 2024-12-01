namespace UnityEngine.Rendering.Universal
{
    internal class ColorGradingUrpPass : ScriptableRenderPass
    {
        public Material material;

        private RenderTargetIdentifier source;
        private RenderTargetIdentifier tempCopy = new RenderTargetIdentifier(tempCopyString);

        private bool maskSet = false;
        private readonly string tag;
        private readonly Color color;
        private readonly float hue;
        private readonly float contrast;
        private readonly float brightness;
        private readonly float saturation;
        private readonly float exposure;
        private readonly float gamma;
        private readonly float sharpness;
        private readonly float blur;
        private readonly Texture2D blurMask;
        private readonly Color vignetteColor;
        private readonly float vignetteAmount;
        private readonly float vignetteSoftness;

        static readonly int tempCopyString = Shader.PropertyToID("_TempTex");
        static readonly int colorString = Shader.PropertyToID("_Color");
        static readonly int hueCosString = Shader.PropertyToID("_HueCos");
        static readonly int hueSinString = Shader.PropertyToID("_HueSin");
        static readonly int hueVectorString = Shader.PropertyToID("_HueVector");
        static readonly int contrastString = Shader.PropertyToID("_Contrast");
        static readonly int brightnessString = Shader.PropertyToID("_Brightness");
        static readonly int saturationString = Shader.PropertyToID("_Saturation");
        static readonly int centralFactorString = Shader.PropertyToID("_CentralFactor");
        static readonly int sideFactorString = Shader.PropertyToID("_SideFactor");
        static readonly int blurString = Shader.PropertyToID("_Blur");
        static readonly int blurMaskString = Shader.PropertyToID("_MaskTex");
        static readonly int vignetteColorString = Shader.PropertyToID("_VignetteColor");
        static readonly int vignetteAmountString = Shader.PropertyToID("_VignetteAmount");
        static readonly int vignetteSoftnessString = Shader.PropertyToID("_VignetteSoftness");

        static readonly string blurKeyword = "BLUR";
        static readonly string shaprenKeyword = "SHARPEN";
        static readonly string vignetteKeyword = "VIGNETTE";

        float cos;

        public ColorGradingUrpPass(RenderPassEvent renderPassEvent, Material material, Color color, float hue, float contrast, float brightness,
            float saturation, float exposure, float gamma, float sharpness, float blur, Texture2D blurMask, Color vignetteColor, float vignetteAmount, float vignetteSoftness, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.material = material;
            this.color = color;
            this.hue = hue;
            this.contrast = contrast;
            this.brightness = brightness;
            this.saturation = saturation;
            this.exposure = exposure;
            this.gamma = gamma;
            this.sharpness = sharpness;
            this.blur = blur;
            this.blurMask = blurMask;
            this.vignetteColor = vignetteColor;
            this.vignetteAmount = vignetteAmount;
            this.vignetteSoftness = vignetteSoftness;
            this.tag = tag;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(tag);
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempCopyString, opaqueDesc, FilterMode.Bilinear);
            cmd.CopyTexture(source, tempCopy);


            material.SetColor(colorString, (Mathf.Pow(2, exposure) - gamma) * color);
            cos = Mathf.Cos(Mathf.Deg2Rad * hue);
            material.SetFloat(hueCosString, cos);
            material.SetFloat(hueSinString, Mathf.Sin(Mathf.Deg2Rad * hue));
            cos = 0.57735f * (1 - cos);
            material.SetVector(hueVectorString, new Vector3(cos, cos, cos));
            material.SetFloat(contrastString, contrast + 1f);
            material.SetFloat(brightnessString, brightness * 0.5f + 0.5f);
            material.SetFloat(saturationString, saturation + 1f);

            if (blur > 0)
            {
                material.EnableKeyword(blurKeyword);
                material.SetFloat(blurString, blur);
                if (!maskSet)
                {
                    material.SetTexture(blurMaskString, blurMask);
                    maskSet = true;
                }
            }
            else
            {
                material.DisableKeyword(blurKeyword);
            }

            if (sharpness > 0)
            {
                material.EnableKeyword(shaprenKeyword);
                material.SetFloat(centralFactorString, 1.0f + (3.2f * sharpness));
                material.SetFloat(sideFactorString, 0.8f * sharpness);
            }
            else
            {
                material.DisableKeyword(shaprenKeyword);
            }

            if (vignetteAmount > 0)
            {
                material.EnableKeyword(vignetteKeyword);
                material.SetColor(vignetteColorString, vignetteColor);
                material.SetFloat(vignetteAmountString, 1 - vignetteAmount);
                material.SetFloat(vignetteSoftnessString, 1 - vignetteSoftness - vignetteAmount);
            }
            else
            {
                material.DisableKeyword(vignetteKeyword);
                material.SetFloat(vignetteAmountString, 1f);
                material.SetFloat(vignetteSoftnessString, 0.999f);
            }

            cmd.Blit(tempCopy, source, material, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempCopyString);
        }
    }
}
