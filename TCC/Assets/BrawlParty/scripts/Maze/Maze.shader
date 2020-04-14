Shader "Hidden/MazeLight"
{
	Properties
	{
		[HideInInspector] _MainTex("Texture", 2D) = "white" {}
		_RenderTexture("Render Texture", 2D) = "black" {}
		_ShadowC("ShadowColor", Color) = (0.1, 0.1, 0.1, 0.1)
		_LightMultiplier("Light Circle Smoothness", Range(0, 5)) = 3
		_ExtraLight("Light Strength", Range(0.5, 5)) = 1
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex, _RenderTexture;
				float4 _ShadowC;

				float _LightMultiplier, _ExtraLight;
				fixed4 frag(v2f i) : SV_Target
				{
				fixed4 lights = (tex2D(_RenderTexture, i.uv)) * _LightMultiplier; // lighting layer
				fixed4 col = tex2D(_MainTex, i.uv); // normal camera view
				lights = saturate(lights); // saturate so it's not extra bright where the lights are
				col = lerp(col * _ShadowC, col , lights * _ExtraLight); // lerp normal view multiplied by shadow color, with normal camera view over the lights layer
				return col;
			}
			ENDCG
		}
		}
}