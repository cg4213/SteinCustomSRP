#ifndef STEIN_LIGHT_INCLUDED
    #define STEIN_LIGHT_INCLUDED
    struct Light
    {
        float3 color;
        float3 direction;
    };

    Light GetDirectionalLight()
    {
        Light light;

        light.color  = float3(0.9,0.9,0.9);
        light.direction  = float3(0,-1,0);
        return light;
    }

#endif