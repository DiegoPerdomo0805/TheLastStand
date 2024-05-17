Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BloodColor ("Blood Color", Color) = (1, 0, 0, 1)
        _SaturationAmount ("Saturation Amount", Range(0, 1)) = 0.5
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
            float4 _BloodColor;
            float _SaturationAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 color = tex2D(_MainTex, i.uv);

                // Calculate grayscale value
                float grayscale = dot(color.rgb, float3(0.299, 0.587, 0.114));

                half3 desaturatedColor = lerp(grayscale * float3(1, 1, 1), color.rgb, step(0.5, length(color.rgb - _BloodColor.rgb)));

                //return half4(desaturatedColor, color.a);

                half3 finalColor = lerp(desaturatedColor, _BloodColor.rgb, step(0.5, length(color.rgb - _BloodColor.rgb)));

                return half4(finalColor, color.a);

                /*// Desaturate the color
                half3 desaturatedColor = lerp(color.rgb, grayscale, _SaturationAmount);

                // Boost blood color intensity
                half3 blood = lerp(desaturatedColor, _BloodColor.rgb, step(0.5, length(color.rgb - _BloodColor.rgb)));

                return half4(blood, color.a);*/
            }
            ENDCG
        }
    }
}
