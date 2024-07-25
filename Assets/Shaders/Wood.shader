Shader "Unlit/Wood"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor("Tint Color", Color) = (0,0,0,1)
        _Color1("Color1", Color) = (0, 0, 0, 1)
        _Color2("Color2", Color) = (0, 0, 0, 1)
        _Seed("Seed", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float4 _Color1;
            float4 _Color2;
            float _Seed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float random(float2 st)
            {
                return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            float noise(float2 st)
            {
                float2 i = floor(st);
                float2 f = frac(st);
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(random(i + float2(0.0, 0.0)),
                                 random(i + float2(1.0, 0.0)), u.x),
                            lerp(random(i + float2(0.0, 1.0)),
                                 random(i + float2(1.0, 1.0)), u.x), u.y);
            }

            float2x2 rotate2d(float angle)
            {
                return float2x2(cos(angle), -sin(angle),
                                sin(angle), cos(angle));
            }

            float lines(float2 pos, float b)
            {
                float scale = 12.0;
                pos *= scale;
                return smoothstep(0.0, 0.15 + b * 0.5, abs((sin(pos.x * 3.1415) + b * 2.0)) * 0.5);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv.x = i.uv.x + _Seed;
                float2 st = i.uv * 0.5;
                st.x = st.x * 0.15;
                            //st.y *= iResolution.y / iResolution.x;

                float2 pos = st.xy * float2(10.0, 3.0);
                            // Add noise
                pos = mul(rotate2d(noise(pos)), pos);
                            // Draw lines
                float pattern = lines(pos, 0.5);
                float4 color1 = _Color1 * pattern;
                float4 color2 = _Color2 * (1.0 - pattern);
                return float4(color1 + color2);
            }
            ENDCG
        }
    }
}
