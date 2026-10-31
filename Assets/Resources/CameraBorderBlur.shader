Shader "Game/UI/UnifiedBlurBorders"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
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
                fixed4 color : COLOR;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _GlobalUniversalBlurTexture;
            float4 _UnifiedBorderTextureViewport;

            v2f vert(appdata input)
            {
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = input.uv;
                output.color = input.color;
                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                // RawImage UVs select the matching region of one enlarged image.
                // URP can expose either a viewport-sized or a display-sized target.
                float4 viewport = _UnifiedBorderTextureViewport;
                float2 uv = viewport.xy + input.uv * viewport.zw;
                fixed3 color = tex2D(_GlobalUniversalBlurTexture, saturate(uv)).rgb;
                return fixed4(color * input.color.rgb, input.color.a);
            }
            ENDCG
        }
    }
}
