#ifndef STEIN_UNLIT_PASS_INCLUDED
#define STEIN_UNLIT_PASS_INCLUDED

#include "Utility.hlsl"

float4 _BaseColor;

float4 UnlitPassVertex(float3 positionOS:POSITION) :SV_POSITION
{
    float4 positionWS = float4(TransformObjectToWorld(positionOS),1.0);
    return TransformWorldToHClip(positionWS);
}

float4 UnlitPassFragment(float4 positionSV:SV_POSITION): SV_TARGET
{
    return _BaseColor;
}
#endif