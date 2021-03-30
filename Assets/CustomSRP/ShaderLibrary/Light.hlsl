#ifndef STEIN_LIGHT_INCLUDED
    #define STEIN_LIGHT_INCLUDED
    #define MAX_DIR_LIGHT_NUM 4

    CBUFFER_START(_CustomLight)
    int _DirectionalLightCount;
    float3 _DirectionalLightColor[MAX_DIR_LIGHT_NUM];
    float3 _DirectionalLightDirection[MAX_DIR_LIGHT_NUM];
    CBUFFER_END

    struct Light
    {
        float3 color;
        float3 direction;
    };

    
    Light GetDirectionalLight(int lightIndex)
    {
        Light light;

        light.color = _DirectionalLightColor[lightIndex];
        light.direction = _DirectionalLightDirection[lightIndex];
        return light;
    }

    int GetDirectionalLightCount()
    {
        return _DirectionalLightCount;
    }

#endif