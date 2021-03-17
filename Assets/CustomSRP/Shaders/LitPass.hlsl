#ifndef STEIN_LIT_PASS_INCLUDED
    #define STEIN_LIT_PASS_INCLUDED

    #include "../ShaderLibrary/Common.hlsl"
    #include "../ShaderLibrary/Surface.hlsl"

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
        float3 normalOS:NORMAL;
        float2 uv:TEXCOORD0;
    };

    struct v2f
    {
        float4 positionSV:SV_POSITION;
        float3 normalWS:VAR_NORMAL;
        float2 uv:VAR_BASE_UV;
        UNITY_VERTEX_INPUT_INSTANCE_ID//instancing index
    };



    v2f LitPassVertex(appData inputApp) 
    {
        v2f output;

        UNITY_SETUP_INSTANCE_ID(inputApp);
        UNITY_TRANSFER_INSTANCE_ID(inputApp,output);//copy instancing index to output

        float3 positionWS = TransformObjectToWorld(inputApp.positionOS);
        output.positionSV =TransformWorldToHClip(positionWS);
        output.normalWS = TransformObjectToWorldNormal(inputApp.normalOS);
        float4 st = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseMap_ST);
        output.uv = inputApp.uv*st.xy +st.zw;
        return output;
    }
    //显示世界空间的法线
    float4 LitPassFragment_NormalWS(v2f input): SV_TARGET
    {
        UNITY_SETUP_INSTANCE_ID(input);
        float4 tex = SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,input.uv);
        float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseColor);
        float4 color = baseColor*tex;
        #if defined(_CLIPPING) 
            clip(color.a-UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_AlphaCutOff));
        #endif
        // color.rgb = input.normalWS;//错误的差值
        // color.rgb = abs(length( input.normalWS)-1)*10;//错误差值可视化
        color.rgb = normalize(input.normalWS);

        return color;
    }

    float4 LitPassFragment(v2f input): SV_TARGET
    {
        UNITY_SETUP_INSTANCE_ID(input);
        float4 tex = SAMPLE_TEXTURE2D(_BaseMap,sampler_BaseMap,input.uv);
        float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseColor);
        float4 color = baseColor*tex;
        #if defined(_CLIPPING) 
            clip(color.a-UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_AlphaCutOff));
        #endif

        Surface surface;
        surface.normal = normalize(input.normalWS);
        surface.color = baseColor.rgb;
        surface.alpha = baseColor.a;

        return float4(surface.color,surface.alpha);
    }
#endif