Shader "Custom/TextureGradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 0, 0, 1)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1)
        _Midpoint ("Gradient Midpoint (-0.5 - 1.5)", Range(-0.5, 1.5)) = 0.5
        _Smoothness ("Smoothness (0.01 - 1000)", Range(0.01, 1000)) = 1.0
        _Rotation ("Rotation (Degrees)", Range(0, 360)) = 0

        _Stencil("Stencil Reference", Float) = 0
        _StencilComp("Stencil Comparison", Float) = 8
        _StencilOp("Stencil Operation", Float) = 0
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            Stencil {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }
 
            ColorMask[_ColorMask]

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;
            float _Midpoint;
            float _Smoothness;
            float _Rotation;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Convert rotation from degrees to radians
                float angle = radians(_Rotation);
                float2 center = float2(0.5, 0.5);

                // Translate UV to center
                float2 uv = i.uv - center;

                // Apply rotation
                float2x2 rotMatrix = float2x2(cos(angle), -sin(angle),
                                              sin(angle), cos(angle));
                uv = mul(rotMatrix, uv);

                // Translate back
                uv += center;

                // Use rotated y for gradient blend
                float t = uv.y;

                // Smoothstep gradient
                float blend = smoothstep(_Midpoint - 0.5 / _Smoothness, _Midpoint + 0.5 / _Smoothness, t);

                // Gradient color
                fixed4 gradientColor = lerp(_Color1, _Color2, blend);

                // Sample sprite texture
                float4 texColor = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));

                // Multiply gradient with texture alpha (preserve gradient alpha)
                return gradientColor * texColor.a;
            }
            ENDCG
        }
    }
}
