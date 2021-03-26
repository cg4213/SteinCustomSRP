using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    public class Lighting
    {
        const string BufferName = "Lighting";
        CommandBuffer buffer = new CommandBuffer() { name = BufferName };
        int dirLightColorId = Shader.PropertyToID("_DirectionalLightColor");
        int dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");

        ScriptableRenderContext context;
        public void Setup(ScriptableRenderContext context)
        {
            this.context = context;

            buffer.BeginSample(BufferName);
            SetupDirectionalLight();
            buffer.EndSample(BufferName);
            context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        void SetupDirectionalLight()
        {
            var light = RenderSettings.sun;
            buffer.SetGlobalVector(dirLightColorId, light.color.linear * light.intensity);
            buffer.SetGlobalVector(dirLightDirectionId, -light.transform.forward);
        }
    }

}