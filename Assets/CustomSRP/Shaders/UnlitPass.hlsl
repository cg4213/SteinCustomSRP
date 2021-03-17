#ifndef STEIN_UNLIT_PASS_INCLUDED
    #define STEIN_UNLIT_PASS_INCLUDED

    #include "../ShaderLibrary/Common.hlsl"

    TEXTURE2D(_BaseMap);
    SAMPLER(sampler_BaseMap);

    UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
    UNITY_DEFINE_INSTANCED_PROP(float4,_BaseMap_ST)
    UNITY_DEFINE_INSTANCED_PROP(float4,_BaseColor)
    UNITY_DEFINE_INSTANCED_PROP(float,_AlphaCutOff)
    UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

    struct appData
    {
        UNITY_VERTEX_INPUT_INSTANCE_ID//instancing index
        float3 positionOS:POSITION;
        float2 uv:TEXCOORD0;
    };

    struct v2f
    {
        float4 positionSV:SV_POSITION;
        float2 uv:VAR_BASE_UV;
        UNITY_VERTEX_INPUT_INSTANCE_ID//instancing index
    };

    v2f UnlitPassVertex(appData input) 
    {
        v2f output;

        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_TRANSFER_INSTANCE_ID(input,output);//copy instancing index to output

        float3 positionWS = TransformObjectToWorld(input.positionOS);
        output.positionSV =TransformWorldToHClip(positionWS);
        float4 st = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseMap_ST);
        output.uv = input.uv*st.xy +st.zw;
        return output;
    }

    float4 UnlitPassFragment(v2f input): SV_TARGET
    {
        UNITY_SETUP_INSTANCE_ID(input);
        float4 tex = SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,input.uv);
        float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseColor);
        float4 color = baseColor*tex;
        #if defined(_CLIPPING) 
            clip(color.a-UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_AlphaCutOff));
        #endif
        return color;
    }
#endif