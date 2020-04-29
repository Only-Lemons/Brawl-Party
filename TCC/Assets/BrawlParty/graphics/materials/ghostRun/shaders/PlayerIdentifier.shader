Shader "BrawlParty/PlayerIdentifier"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"{}
		[HDR] _Color ("Main Color", Color) = (1,1,1,1)

	}
		SubShader
	{
		Tags {"RenderType" = "Transparent"}

		Blend One One
		ZWrite Off

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv0 : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv0 : TEXCOOD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;

			v2f vert (appdata v)
			{
				v2f o;

				o.uv0 = TRANSFORM_TEX(v.uv0, _MainTex);
				o.position = UnityObjectToClipPos(v.vertex);

				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				float4 tex = tex2D(_MainTex, i.uv0);
				tex *= _Color;

				return tex;
			}

			ENDCG
		}
	}
}