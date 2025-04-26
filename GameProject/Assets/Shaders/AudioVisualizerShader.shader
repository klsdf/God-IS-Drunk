Shader "Custom/AudioVisualizerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BarsData ("Bars Data", Vector) = (0,0,0,0)
        _BarCount ("Bar Count", Float) = 64
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _BarsData[64];
            float _BarCount;
            float4 _Color;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float barIndex = floor(uv.x * _BarCount);
                float barHeight = _BarsData[barIndex].x; // 从频率数据读高度喵

                if (uv.y < barHeight)
                    return _Color;
                else
                    return float4(0,0,0,0);
            }
            ENDCG
        }
    }
}
