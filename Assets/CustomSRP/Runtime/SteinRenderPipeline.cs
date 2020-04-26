using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    public class SteinRenderPipeline : RenderPipeline
    {
        BaiscCameraRender m_basicCameraRender = new BaiscCameraRender ("Baisc Render");
        protected override void Render (ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var cam in cameras)
            {
                m_basicCameraRender.Render (context, cam);
            }
        }

    }

}