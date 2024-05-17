Shader "Custom/RedHighlight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RedThreshold ("Red Threshold", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
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
            float _RedThreshold;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Convert to grayscale
                float gray = dot(col.rgb, float3(0.2126, 0.7152, 0.0722));
                fixed3 grayscale = float3(gray, gray, gray);

                // Check if the color is close to red
                float redness = col.r - max(col.g, col.b);

                // Apply the effect: red objects stay red, everything else becomes grayscale
                col.rgb = lerp(grayscale, col.rgb, step(_RedThreshold, redness));
                

                return col;
            }
            ENDCG
        }
    }
}
