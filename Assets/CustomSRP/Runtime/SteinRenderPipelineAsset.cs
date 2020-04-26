using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    [CreateAssetMenu]
    public class SteinRenderPipelineAsset : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline ()
        {
            return new SteinRenderPipeline ();
        }
    }

}