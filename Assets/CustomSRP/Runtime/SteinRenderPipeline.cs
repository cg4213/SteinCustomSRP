using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    public class SteinRenderPipeline : RenderPipeline
    {
        BaiscCameraRender m_basicCameraRender = new BaiscCameraRender("Baisc Render");
        Lighting m_lighting = new Lighting();
        public bool enableInstancing = true;
        public bool enableDynamicBatching = true;
        public SteinRenderPipeline(bool enableSRPBatcher, bool enableInstancing, bool enableDynamicBatching)
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = enableSRPBatcher;
            this.enableInstancing = enableInstancing;
            this.enableDynamicBatching = enableDynamicBatching;
        }
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            this.m_lighting.Setup(context);

            foreach (var cam in cameras)
            {
                m_basicCameraRender.Render(context, cam, enableInstancing, enableDynamicBatching);
            }
        }

    }

}