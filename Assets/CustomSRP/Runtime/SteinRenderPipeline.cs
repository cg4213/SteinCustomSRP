using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    public class SteinRenderPipeline : RenderPipeline
    {
        BaiscCameraRender m_basicCameraRender = new BaiscCameraRender("Baisc Render");

        public bool enableInstancing = true;
        public bool enableDynamicBatching = true;
        public SteinRenderPipeline(bool enableSRPBatcher, bool enableInstancing, bool enableDynamicBatching)
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = enableSRPBatcher;
            this.enableInstancing = enableInstancing;
            this.enableDynamicBatching = enableDynamicBatching;
            GraphicsSettings.lightsUseLinearIntensity = true; //light 的finalColor默认不是linear，这里需要开
        }
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {

            foreach (var cam in cameras)
            {
                m_basicCameraRender.Render(context, cam, enableInstancing, enableDynamicBatching);
            }
        }

    }

}