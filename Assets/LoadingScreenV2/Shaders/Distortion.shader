Shader "Custom/Distortion"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _MainColor ("Color", Color) = (1, 1, 1, 1)
        _DistortionMap ("Distortion Map (Normal)", 2D) = "bump" {}
        _ScrollDirection ("Distortion Scroll Direction (XY)", Vector) = (0.02, 0.02, 0, 0)
        _DistortionStrength ("Distortion Strength", Float) = 0.05
        _DistortionOffset ("Distortion Offset", Vector) = (0.5, 0.5, 0, 0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainColor;
            sampler2D _DistortionMap;
            float4 _ScrollDirection;
            float _DistortionStrength;
            float4 _DistortionOffset;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvDistort : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uvDistort = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 distortionUV = i.uvDistort + _ScrollDirection.xy * _Time.y;

                // Sample distortion normal map
                float2 distortion = tex2D(_DistortionMap, distortionUV).rg;

                // Center distortion around 0
                distortion = (distortion - _DistortionOffset.xy) * 2.0;

                // Apply distortion to base UVs
                float2 distortedUV = i.uv + distortion * _DistortionStrength;

                fixed4 col = tex2D(_MainTex, distortedUV);
                return col * _MainColor;
            }
            ENDCG
        }
    }
}
