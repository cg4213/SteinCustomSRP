#ifndef STEIN_UNLIT_PASS_INCLUDED
#define STEIN_UNLIT_PASS_INCLUDED

#include "Utility.hlsl"

CBUFFER_START(UnityPerMaterial)
float4 _BaseColor;
CBUFFER_END

float4 UnlitPassVertex(float3 positionOS:POSITION) :SV_POSITION
{
    float3 positionWS = TransformObjectToWorld(positionOS);
    return TransformWorldToHClip(positionWS);
}

float4 UnlitPassFragment(float4 positionSV:SV_POSITION): SV_TARGET
{
    return _BaseColor;
}
#endif