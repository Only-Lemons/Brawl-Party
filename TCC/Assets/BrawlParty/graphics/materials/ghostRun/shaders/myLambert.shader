Shader "myShader/myLambert"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"{}
		_Color ("Main Color", Color) = (1,1,1,1)
		_RimColor ("Rim Light Color", Color) = (1,1,1,1)

		[HDR]
		_AmbColor ("Ambient Color", Color) = (0.5, 0.5, 0.5, 1)
	}
		SubShader
	{
		Tags {"RenderType" = "Opaque"}

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
            #include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float uv0 : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv0 : TEXCOOD0;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;

			fixed4 _Color, _AmbColor, _RimColor;

			v2f vert (appdata v)
			{
				v2f o;

				o.uv0 = v.uv0;
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.position = UnityObjectToClipPos(v.vertex);

				return o;
			}

			float Posterize (float steps, float val)
			{
				return floor(val * steps) / steps;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv0;
				float3 normal = normalize(i.normal);

				//Lighting NdotL
				float3 lightDir = _WorldSpaceLightPos0;
				float3 lightColor = _LightColor0.rgb;
				float lightFalloff = max(0, dot(normal, lightDir));

				//float cell = Posterize(3, lightFalloff);

				float3 directDiffuseLight = lightColor * lightFalloff;

				float3 ambientLight = _AmbColor.rgb; //Ambient Light Color

				//Composite
				float3 diffuseLight = ambientLight + directDiffuseLight;
				diffuseLight *= _Color.rgb;

				return float4(diffuseLight, 1);
			}

			ENDCG
		}
	}
}