Shader "URP/PolyArtMaskTint"
{
    Properties
    {
        _PolyArtAlbedo("Albedo", 2D) = "white" {}
        _PolyArtMask("Mask", 2D) = "white" {}

        _Hair("Hair", Color) = (1, 0.6, 0, 1)
        _InnerCloth("InnerCloth", Color) = (0.25, 0, 0.78, 1)
        _OuterChlothes("OuterChlothes", Color) = (0.07, 0.4, 0.93, 1)

        _Metallic("Metallic", Range(0,1)) = 0
        _Smoothness("Smoothness", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _PolyArtAlbedo;
            sampler2D _PolyArtMask;
            float4 _PolyArtAlbedo_ST;
            float4 _PolyArtMask_ST;

            float4 _Hair;
            float4 _InnerCloth;
            float4 _OuterChlothes;

            float _Metallic;
            float _Smoothness;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 albedoUV = TRANSFORM_TEX(IN.uv, _PolyArtAlbedo);
                float2 maskUV = TRANSFORM_TEX(IN.uv, _PolyArtMask);

                float4 albedo = tex2D(_PolyArtAlbedo, albedoUV);
                float4 mask = tex2D(_PolyArtMask, maskUV);

                float4 hairBlend = min(mask.b, _Hair);
                float4 innerBlend = min(mask.g, _InnerCloth);
                float4 outerBlend = min(mask.r, _OuterChlothes);

                float4 blend = hairBlend + innerBlend + outerBlend;
                float4 final = lerp(albedo, saturate(albedo * blend) * 2.0, mask.r + mask.g + mask.b);

                // Output color with smoothness and metallic encoded in alpha/blue
                return float4(final.rgb, 1.0);
            }
            ENDHLSL
        }
    }

    FallBack Off
}
