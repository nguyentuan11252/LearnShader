Shader "Unlit/Grass"
{
    Properties
    {
        
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
                float4 color: COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color: COLOR;
            };
            float3 _CarvePosition;
            float2 _CarveRadius;

            v2f vert(appdata v)
            {
                v2f o;
                float t = _CosTime.w * 0.5 + 0.5;
                float x = v.vertex.x * t + v.uv.x * (1 - t);
                //t = _SinTime.w * 0.5 + 0.5;
                float z = v.vertex.z * t + v.uv.y * (1 - t);

                float4 newPos = float4(x,v.vertex.y,z,1.0);
                o.vertex = UnityObjectToClipPos(newPos);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = i.color;
                return col;
            }
            ENDCG
        }
    }
}
