Shader "Custom/URPGradientCube"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _ColorStart ("Start Color", Color) = (1,0,0,1)
        _ColorEnd ("End Color", Color) = (0,0,1,1)
        _GradientOffset ("Gradient Offset", Range(-1, 1)) = 0
        _GradientScale ("Gradient Scale", Range(0.1, 2)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 localPos : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            float4 _BaseColor;
            float4 _ColorStart;
            float4 _ColorEnd;
            float _GradientOffset;
            float _GradientScale;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.localPos = v.positionOS.xyz;
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                
                // 计算渐变因子
                float zNormalized = (i.localPos.z + _GradientOffset) * _GradientScale;
                float zFactor = saturate(zNormalized * 0.5 + 0.5);
                
                // 插值颜色（包括 Alpha）
                float4 gradientColor = lerp(_ColorStart, _ColorEnd, zFactor);
                
                // 最终颜色 = 纹理颜色 × 渐变颜色（RGB 和 Alpha 都相乘）
                return texColor * gradientColor;
            }
            ENDHLSL
        }
    }
}