using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{

    public partial class BaiscCameraRender
    {
        string m_sampleName;
        string m_bufferName;

        ScriptableRenderContext context;
        Camera camera;
        CommandBuffer buffer;
        private CullingResults cullingresult;

        ShaderTagId m_testUnlitTagId = new ShaderTagId("SRPDefaultUnlit");
        ShaderTagId m_CustomLitTagId = new ShaderTagId("CustomLit");
        public BaiscCameraRender(string name)
        {
            this.m_bufferName = name;
            this.m_sampleName = name;

            this.buffer = new CommandBuffer
            {
                name = m_bufferName
            };

        }
        void Setup()
        {
            // buffer.name = camera.name;
            buffer.BeginSample(m_sampleName);
            context.SetupCameraProperties(camera);

            var clearFlags = camera.clearFlags;
            buffer.ClearRenderTarget(
                clearFlags < CameraClearFlags.Depth,
                clearFlags == CameraClearFlags.Color,
                //感觉这个判断没啥意义，在其他情况下clearcolor根本不重要
                //clearColor在线性空间下也需要转换为线性
                clearFlags == CameraClearFlags.Color? camera.backgroundColor.linear : Color.clear);

            ExecuteCommand();
        }

        void DrawGeometry(bool enableInstancing, bool enableDynamicBatching)
        {

            var sortingSettings = new SortingSettings(this.camera);

            var drawingSettings = new DrawingSettings(m_testUnlitTagId, sortingSettings)
            {
                enableDynamicBatching = enableDynamicBatching,
                enableInstancing = enableInstancing
            };
            drawingSettings.SetShaderPassName(1, m_CustomLitTagId);

            // this.cullingresult.visibleLights

            //what to draw
            var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

            //opaque 是front to back，可以cull掉一些frag计算
            sortingSettings.criteria = SortingCriteria.CommonOpaque;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.opaque;
            context.DrawRenderers(this.cullingresult, ref drawingSettings, ref filteringSettings);

            //少画一点天空盒？
            context.DrawSkybox(this.camera);

            //transparent 是back to front，因为要混合
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings; //struct 需要重新设置才有效
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;
            // transparent 不写入深度，所以会被任何在其后的渲染覆盖
            context.DrawRenderers(this.cullingresult, ref drawingSettings, ref filteringSettings);
        }

        bool Cull()
        {
            //视锥体内的判断
            //cull，目前是默认的cull
            if (camera.TryGetCullingParameters(out ScriptableCullingParameters scriptableCullingParameters))
            {
                // what can be drawn
                cullingresult = context.Cull(ref scriptableCullingParameters);
                return true;
            }
            else
            {
                //cull failed
                Debug.LogWarning($"camera {camera} get culling params failed");
                return false;
            }

        }
        void Submit()
        {
            buffer.EndSample(m_sampleName);
            ExecuteCommand();

            context.Submit();

        }

        private void ExecuteCommand()
        {
            context.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        public void Render(ScriptableRenderContext context, Camera camera, bool enableInstancing, bool enableDynamicBatching)
        {
            this.context = context;
            this.camera = camera;

#if UNITY_EDITOR
            PrepareBuffer();
            //需要在cull之前，否则可能因为没有可渲染内容而跳过
            PrepareForSceneWindow();
#endif
            //skip when nothing to render
            if (!Cull())
                return;

            Setup();

            DrawGeometry(enableInstancing, enableDynamicBatching);
#if UNITY_EDITOR
            DrawUnsupportedShaders();
            DrawGizmos();
#endif
            Submit();

        }
    }

}