#ifndef STEIN_UNLIT_PASS_INCLUDED
#define STEIN_UNLIT_PASS_INCLUDED

#include "Utility.hlsl"

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
    UNITY_DEFINE_INSTANCED_PROP(float4,_BaseColor)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

struct appData
{
    UNITY_VERTEX_INPUT_INSTANCE_ID
    float3 positionOS:POSITION;
};

struct v2f
{
    float4 positionSV:SV_POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

v2f UnlitPassVertex(appData input) 
{
    v2f output;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input,output);

    float3 positionWS = TransformObjectToWorld(input.positionOS);
    output.positionSV =TransformWorldToHClip(positionWS);

    return output;
}

float4 UnlitPassFragment(v2f input): SV_TARGET
{

    UNITY_SETUP_INSTANCE_ID(input);
    return UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial,_BaseColor);
}
#endif