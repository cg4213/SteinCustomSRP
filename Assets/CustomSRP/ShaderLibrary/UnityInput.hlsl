#ifndef STEIN_UNITY_INPUT_INCLUDED
    #define STEIN_UNITY_INPUT_INCLUDED

    //CBUFFER_START defined in Core RP lib,for multi platform cbuffer delcaration
    //cbuffer make shader compatible with srp batcher
    CBUFFER_START(UnityPerDraw)
    float4x4 unity_ObjectToWorld;
    float4x4 unity_WorldToObject;
    float4 unity_LODFade;
    real4 unity_WorldTransformParams;
    CBUFFER_END

    float4x4 unity_MatrixV;
    float4x4 unity_MatrixVP;
    float4x4 glstate_matrix_projection;

    #define UNITY_MATRIX_M unity_ObjectToWorld
    #define UNITY_MATRIX_I_M unity_WorldToObject
    #define UNITY_MATRIX_V unity_MatrixV
    #define UNITY_MATRIX_VP unity_MatrixVP
    #define UNITY_MATRIX_P glstate_matrix_projection

#endif