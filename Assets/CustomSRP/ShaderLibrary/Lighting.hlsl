#ifndef STEIN_LIGHTING_INCLUDED
    #define STEIN_LIGHTING_INCLUDED
    #include "./Surface.hlsl"
    #include "./Light.hlsl"
    
    float3 IncommingLight(Surface surface, Light light)
    {
        return saturate(dot(surface.normal, light.direction)) * light.color;
    }
    float3 GetLighting(Surface surface, Light light)
    {
        return IncommingLight(surface, light) * surface.color;
    }
    

    float3 GetLighting(Surface surface)
    {
        float3 lightingColor = 0;
        for (int index = 0; index < GetDirectionalLightCount(); index++)
        {
            lightingColor = lightingColor + GetLighting(surface, GetDirectionalLight(index));
        }
        return lightingColor;
    }
    

#endif