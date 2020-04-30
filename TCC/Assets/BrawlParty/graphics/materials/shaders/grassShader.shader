Shader "Custom/grassShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

		[Header(Movement Params)]
		_Noise ("Movement Noise", 2D) = "black" {}
		_Offset ("Vertex Movement Offset", Range(0,4)) = 0.5
		_Speed ("Grass Speed", Range(0, 10)) = 2
		_Frequency ("Grass Movement Frequency", Range(0, 30)) = 2
		_Amplitude ("Grass Movement Amplitude", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert
		#pragma vertex vert

        struct Input
        {
            float2 uv_MainTex;
        };

		sampler2D _MainTex, _Noise;
		float _Offset, _Speed, _Frequency, _Amplitude;
        fixed4 _Color;
		float4 _Noise_ST;

        void vert (inout appdata_full v)
		{
			float4 modifiedPos = v.vertex;
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);

			float2 noise = TRANSFORM_TEX(worldPos.xy, _Noise);

			float offset = worldPos.y * _Offset;
			modifiedPos.x += ((sin(v.vertex.y * _Frequency + _Time.y * _Speed) * 0.5 + 0.5) * _Amplitude);

			v.vertex = modifiedPos;
		}

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
