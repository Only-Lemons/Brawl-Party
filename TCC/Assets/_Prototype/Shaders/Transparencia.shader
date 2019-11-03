Shader "Custom/Transparencia"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AlphaNivel ("Valor do Alpha", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"
		"IgnoreProjector" = "True"
		"Queue" = "Transparent" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows alpha:blend
		#pragma target 5.0


        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _AlphaNivel;

        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			o.Alpha = _AlphaNivel;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
