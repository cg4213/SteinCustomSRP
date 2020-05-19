#ifndef STEIN_UTILITY_INCLUDED
#define STEIN_UTILITY_INCLUDED
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "UnityInput.hlsl"


#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

// float3 TransformObjectToWorld(float3 position)
// {
//     return mul(unity_ObjectToWorld,float4(position,1.0)).xyz;
// }

// float4 TransformWorldToHClip(float4 positionW)
// {
//     return mul(unity_MatrixVP,positionW);
// }

// #define TRANSFORM_OBJ_TO_WORLD(float3 position)  mul(unity_ObjectToWorld,float4(position,1.0)).xyz
#endif