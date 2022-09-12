Shader "Unlit/bai4"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _Rotation("Rotation", float) = 0
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Rotation;

            float2 RotateMat(float2 uv, float t) {
                float2 newUv;
                //scale
                newUv.xy = (((uv.xy - 0.5) * (sin((t)) * 3 + 4)) + 0.5);

                //rotate
                newUv.xy = newUv.xy * 2 - 1;
                float c = cos(_Rotation + t);
                float s = sin(_Rotation + t);
                float2x2 mat = float2x2(c, -s, s, c);
                newUv.xy = mul(newUv.xy, mat);
                newUv.xy = newUv.xy * 0.5 + 0.5f;
                return newUv;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = RotateMat(v.uv.xy, _Time.y);
                return o;
            }

            fixed4 calculate(float2 uv) {
                fixed4 col;

                if (uv.x < 0 || uv.y < 0 || uv.x > 1 || uv.y > 1) {
                    col = tex2D(_MainTex, float2(0, 0));
                }
                else {
                    col = tex2D(_MainTex, uv.xy);
                }
                return col;
            }
           
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = calculate(i.uv);

                return col;
            }
            ENDCG
        }
    }
}
