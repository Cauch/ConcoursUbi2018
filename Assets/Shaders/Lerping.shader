﻿// Toony Colors Free
// (c) 2012,2016 Jean Moreno


// Want more features ? Check out Toony Colors Pro+Mobile 2 !
// http://www.jeanmoreno.com/toonycolorspro/


Shader "Toony Colors Free/Lerping"
{
	Properties
	{
		//TOONY COLORS
		_Color("Color", Color) = (0.5,0.5,0.5,1.0)
		_HColor("Highlight Color", Color) = (0.6,0.6,0.6,1.0)
		_SColor("Shadow Color", Color) = (0.3,0.3,0.3,1.0)

		//DIFFUSE
		_MainTex("Main Texture (RGB)", 2D) = "white" {}
		//SECOND TEXT
		_SecTex("Surprise Tex (RGB)", 2D) = "white" {}

		_Value("Percentage of text 1 vs 2 ", Range(0,1)) = 0.0

	//TOONY COLORS RAMP
	_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}

	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque" }

		CGPROGRAM

#pragma surface surf ToonyColorsCustom
#pragma target 2.0
#pragma glsl


		//================================================================
		// VARIABLES

		fixed4 _Color;
	sampler2D _MainTex;
	sampler2D _SecTex;
	half _Value;

	struct Input
	{
		half2 uv_MainTex;
		half2 uv_SecTex;
	};

	//================================================================
	// CUSTOM LIGHTING

	//Lighting-related variables
	fixed4 _HColor;
	fixed4 _SColor;
	sampler2D _Ramp;

	//Custom SurfaceOutput
	struct SurfaceOutputCustom
	{
		fixed3 Albedo;
		fixed3 Normal;
		fixed3 Emission;
		half Specular;
		fixed Alpha;
	};

	inline half4 LightingToonyColorsCustom(SurfaceOutputCustom s, half3 lightDir, half3 viewDir, half atten)
	{
		s.Normal = normalize(s.Normal);
		fixed ndl = max(0, dot(s.Normal, lightDir)*0.5 + 0.5);

		fixed3 ramp = tex2D(_Ramp, fixed2(ndl,ndl));
#if !(POINT) && !(SPOT)
		ramp *= atten;
#endif
		_SColor = lerp(_HColor, _SColor, _SColor.a);	//Shadows intensity through alpha
		ramp = lerp(_SColor.rgb,_HColor.rgb,ramp);
		fixed4 c;
		c.rgb = s.Albedo * _LightColor0.rgb * ramp;
		c.a = s.Alpha;
#if (POINT || SPOT)
		c.rgb *= atten;
#endif
		return c;
	}


	//================================================================
	// SURFACE FUNCTION

	void surf(Input IN, inout SurfaceOutputCustom o)
	{
		fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
		fixed4 secTex = tex2D(_SecTex, IN.uv_SecTex);
		fixed4 finalTex = (1.0 - _Value)*mainTex + (_Value*secTex);
		o.Albedo = finalTex.rgb * _Color.rgb;
		o.Alpha = finalTex.a * _Color.a;

	}

	ENDCG
	}

		Fallback "Diffuse"
		CustomEditor "TCF_MaterialInspector"
}