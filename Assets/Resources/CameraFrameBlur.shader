Shader "Game/Camera/FrameBlur"
{
    Properties { _MainTex ("Camera Frame", 2D) = "white" {} }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
            sampler2D _MainTex;
            float4 _BlurStep;
            fixed4 frag(v2f_img input) : SV_Target
            {
                float2 uv = input.uv;
                float2 offset = _BlurStep.xy;
                fixed4 color = tex2D(_MainTex, uv) * 0.227027;
                color += tex2D(_MainTex, uv + offset * 1.384615) * 0.316216;
                color += tex2D(_MainTex, uv - offset * 1.384615) * 0.316216;
                color += tex2D(_MainTex, uv + offset * 3.230769) * 0.070270;
                color += tex2D(_MainTex, uv - offset * 3.230769) * 0.070270;
                return fixed4(color.rgb, 1);
            }
            ENDCG
        }
    }
}
