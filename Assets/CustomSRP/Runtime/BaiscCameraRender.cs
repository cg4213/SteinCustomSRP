using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{

    public partial class BaiscCameraRender
    {
        string m_bufferName;
        public BaiscCameraRender (string name)
        {
            this.m_bufferName = name;

            this.buffer = new CommandBuffer
            {
                name = m_bufferName
            };

        }

        ScriptableRenderContext context;
        Camera camera;
        CommandBuffer buffer;
        private CullingResults cullingresult;

        ShaderTagId m_testUnlitTagId = new ShaderTagId ("SRPDefaultUnlit");

        void Setup ()
        {
            // buffer.name = camera.name;
            buffer.BeginSample (m_bufferName);
            context.SetupCameraProperties (camera);
            buffer.ClearRenderTarget (
                (camera.clearFlags & CameraClearFlags.Depth) != 0,
                (camera.clearFlags & CameraClearFlags.Color) != 0,
                camera.backgroundColor);

            ExecuteCommand ();
        }

        void DrawGeometry ()
        {

            var sortingSettings = new SortingSettings (this.camera);

            var drawingSettings = new DrawingSettings (m_testUnlitTagId, sortingSettings);

            //what to draw
            var filteringSettings = new FilteringSettings (RenderQueueRange.opaque);

            //opaque 是front to back，可以cull掉一些frag计算
            sortingSettings.criteria = SortingCriteria.CommonOpaque;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.opaque;
            context.DrawRenderers (this.cullingresult, ref drawingSettings, ref filteringSettings);

            //少画一点天空盒？
            context.DrawSkybox (this.camera);

            //transparent 是back to front，因为要混合
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings; //struct 需要重新设置才有效
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;
            // transparent 不写入深度，所以会被任何在其后的渲染覆盖
            context.DrawRenderers (this.cullingresult, ref drawingSettings, ref filteringSettings);
        }

        bool Cull ()
        {
            //视锥体内的判断
            //cull，目前是默认的cull
            if (camera.TryGetCullingParameters (out ScriptableCullingParameters scriptableCullingParameters))
            {
                // what can be drawn
                cullingresult = context.Cull (ref scriptableCullingParameters);
                return true;
            }
            else
            {
                //cull failed
                Debug.LogWarning ($"camera {camera} get culling params failed");
                return false;
            }

        }
        void Submit ()
        {
            buffer.EndSample (m_bufferName);
            ExecuteCommand ();

            context.Submit ();

        }

        private void ExecuteCommand ()
        {
            context.ExecuteCommandBuffer (buffer);
            buffer.Clear ();
        }

        public void Render (ScriptableRenderContext context, Camera camera)
        {
            this.context = context;
            this.camera = camera;

            //skip when nothing to render
            if (!Cull ())
                return;

            Setup ();

            DrawGeometry ();
#if UNITY_EDITOR
            DrawUnsupportedShaders ();
            DrawGizmos ();
#endif
            Submit ();

        }
    }

}