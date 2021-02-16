using UnityEngine;
using UnityEngine.Rendering;

namespace Stein.Rendering
{
    [CreateAssetMenu]
    public class SteinRenderPipelineAsset : RenderPipelineAsset
    {
        public bool enableSRPBatcher = true;
        public bool enableInstancing = true;
        public bool enableDynamicBatching = true;
        protected override RenderPipeline CreatePipeline ()
        {
            return new SteinRenderPipeline (enableSRPBatcher, enableInstancing, enableDynamicBatching);
        }
    }

}