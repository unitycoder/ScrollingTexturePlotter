// https://github.com/unitycoder/ScrollingTexturePlotter

Shader "UnityLibrary/Effects/ScrollingPlotter"
{
	Properties
	{
		_MainTex("Plotting Texture", 2D) = "white" {}
	}

	CGINCLUDE

	#include "UnityCustomRenderTexture.cginc"

	sampler2D _MainTex;

	half4 frag(v2f_customrendertexture i) : SV_Target
	{
		//float xs = 1 / _CustomRenderTextureWidth;
		float ys = 1 / _CustomRenderTextureHeight;
		float2 uv = i.globalTexcoord;

		// read pixel below current position
		half3 c = tex2D(_SelfTexture2D, uv - float2(0, ys));

		// read from plotting texture
		half3 p = tex2D(_MainTex, float2(uv));

		// for the bottom row, use our plotting texture
		half b = (uv.y<ys);
		c = lerp(c, p, b);

		return half4(c,1);
	}

	ENDCG

	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			Name "Update"
			CGPROGRAM
			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag
			ENDCG
		}
	}
}
