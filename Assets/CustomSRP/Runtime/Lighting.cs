using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    public class Lighting
    {
        const int maxDirLightCount = 4;
        const string BufferName = "Lighting";
        CommandBuffer buffer = new CommandBuffer() { name = BufferName };
        int dirLightCountId = Shader.PropertyToID("_DirectionalLightCount");
        int dirLightColorId = Shader.PropertyToID("_DirectionalLightColor");
        int dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");
        static Vector4[] dirLightColors = new Vector4[maxDirLightCount];
        static Vector4[] dirLightDirections = new Vector4[maxDirLightCount];
        ScriptableRenderContext context;
        public void Setup(ScriptableRenderContext context, CullingResults cullingResults)
        {
            this.context = context;

            buffer.BeginSample(BufferName);
            SetupDirectionalLight(cullingResults);
            buffer.EndSample(BufferName);
            context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        void SetupDirectionalLight(CullingResults cullingResults)
        {
            var lights = cullingResults.visibleLights;

            int dirLightCount = 0;

            for (int i = 0; i < lights.Length; i++)
            {
                VisibleLight _curLight = lights[i];
                if (_curLight.lightType != LightType.Directional)
                    continue;
                dirLightCount++;
                if (dirLightCount >= maxDirLightCount)
                    break;
                dirLightColors[i] = _curLight.finalColor;
                // dirLightDirections[i] = -_curLight.light.transform.forward;
                dirLightDirections[i] = -_curLight.localToWorldMatrix.GetColumn(3); //显然效率上比找到light.transform更优
            }
            buffer.SetGlobalInt(dirLightCountId, lights.Length);
            buffer.SetGlobalVectorArray(dirLightColorId, dirLightColors);
            buffer.SetGlobalVectorArray(dirLightDirectionId, dirLightDirections);

        }
    }

}