Shader "Custom/HpBar"
{
	Properties{
	 _MainTex("Texture", 2D) = "white" {}
	 _CutoffX("CutoffX", Range(0.0,1.0)) = 1
	 _CutoffY("CutoffY", Range(0.0,1.0)) = 1
	}



		Category{





			Cull off
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off



			Fog { Mode Off }



		SubShader {

			Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}


		CGPROGRAM
		  #pragma surface surf Unlit alpha

		 half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten) {

			  half4 c;
			  c.rgb = s.Albedo;
			  c.a = s.Alpha;
			  return c;
		  }

			sampler2D _MainTex;

		   struct Input {
			float2 uv_MainTex;
			 float4 color : Color;
			 };

		fixed _CutoffX;
		fixed _CutoffY;

		  void surf(Input IN, inout SurfaceOutput o) {

		half4 tex = tex2D(_MainTex, IN.uv_MainTex);

		o.Albedo = IN.color.rgb * tex.rgb;
			o.Alpha = IN.uv_MainTex.x > _CutoffX ? 0 : IN.uv_MainTex.y > _CutoffY ? 0 : IN.color.a * tex.a;

		  }



		  ENDCG


	}

	 }

}
