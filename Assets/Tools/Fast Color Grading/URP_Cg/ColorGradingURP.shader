Shader "SupGames/Mobile/ColorGradingURP"
{
	Properties
	{
		[HideInInspector]_MainTex("Base (RGB)", 2D) = "" {}
	}

	HLSLINCLUDE
	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#define huevec half3(0.57735h, 0.57735h, 0.57735h)
	#define satur half3(0.299h, 0.587h, 0.114h)
	TEXTURE2D_X(_MainTex);
	SAMPLER(sampler_MainTex);
	TEXTURE2D_X(_MaskTex);
	SAMPLER(sampler_MaskTex);
	uniform half4 _Color;
	uniform half _HueCos;
	uniform half _HueSin;
	uniform half3 _HueVector;
	uniform half _Contrast;
	uniform half _Brightness;
	uniform half _Saturation;
	uniform half _Exposure;
	uniform half _Gamma;
	uniform half _Blur;
	uniform half _CentralFactor;
	uniform half _SideFactor;
	uniform half4 _VignetteColor;
	uniform half _VignetteAmount;
	uniform half _VignetteSoftness;
	uniform half4 _MainTex_TexelSize;

	struct appdata
	{
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f
	{
		half4 pos : POSITION;
		half4 uv : TEXCOORD0;
#if defined(SHARPEN)
		half4  uv1 : TEXCOORD1;
#endif
#if defined(BLUR)
		half4  uv2 : TEXCOORD2;
#endif
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2f vert(appdata i)
	{
		v2f o = (v2f)0;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = TransformWorldToHClip(TransformObjectToWorld(i.pos.xyz));
		o.uv.xy = UnityStereoTransformScreenSpaceTex(i.uv);
		o.uv.zw = i.uv - 0.5h;
#if defined(SHARPEN)
		o.uv1 = half4(o.uv.xy - _MainTex_TexelSize.xy, o.uv.xy + _MainTex_TexelSize.xy);
#endif
#if defined(BLUR)
		o.uv2 = half4(o.uv.xy - _MainTex_TexelSize.xy * _Blur, o.uv.xy + _MainTex_TexelSize.xy * _Blur);
#endif
		return o;
	}

	half4 fragFilter(v2f i) : COLOR
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
#if defined(SHARPEN)
		c *= _CentralFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xy) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xw) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zy) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zw) * _SideFactor;
#endif
#if defined(BLUR)
		half4 m = SAMPLE_TEXTURE2D_X(_MaskTex, sampler_MaskTex, i.uv.xy);
		half4 b = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv2.xy);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv2.xw);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv2.zy);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv2.zw);
		c = lerp(c, b * 0.25h, m.r);
#endif
		c.rgb = c.rgb * _HueCos + cross(huevec, c.rgb) * _HueSin + dot(huevec, c.rgb) * _HueVector;
		c.rgb = (c.rgb - 0.5h) * _Contrast + _Brightness;
		c.rgb = lerp(dot(c.rgb, satur), c.rgb, _Saturation) * _Color.rgb;
#if defined(VIGNETTE)
		c.rgb = lerp(_VignetteColor.rgb, c.rgb, smoothstep(_VignetteAmount, _VignetteSoftness, sqrt(dot(i.uv.zw, i.uv.zw))));
#endif
		return c;
	}
	ENDHLSL

	Subshader
	{
		Pass
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  HLSLPROGRAM
		  #pragma vertex vert
		  #pragma fragment fragFilter
		  #pragma shader_feature_local BLUR
		  #pragma shader_feature_local SHARPEN
		  #pragma shader_feature_local VIGNETTE
		  ENDHLSL
		}
	}
	Fallback off
}