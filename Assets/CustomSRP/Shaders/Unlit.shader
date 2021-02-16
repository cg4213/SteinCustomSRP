Shader "SteinRP/Unlit"
{
    Properties
    {
        _BaseMap("Texture",2D) = "white"{}
        _BaseColor("Color" , Color) = (1.0,1.0,1.0,1.0)
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend",float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend",float) = 0
        [Enum(Off,0,On,1)]_ZWrite("Z Write",float) = 1
    }
    SubShader
    {
        // Tags { "RenderType"="Opaque" }
        // LOD 100
        Blend [_SrcBlend] [_DstBlend]
        ZWrite [_ZWrite]
        Pass
        {
            HLSLPROGRAM
            #pragma multi_compile_instancing
            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment
            #include "UnlitPass.hlsl"
          
            ENDHLSL
        }
    }
}
