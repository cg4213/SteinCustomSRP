using System;

namespace UnityEditor.VFX.HDRP
{
    /// <summary>
    /// Read this: https://forum.unity.com/threads/state-of-vfx-graph-for-custom-srps.951059/
    /// </summary>
    internal class VFXCustomSRPBinder : VFXSRPBinder
    {
        public override string templatePath
        {
            get
            {
                return "Assets/CustomVFXGraph/Editor/Shaders/CustomRP";
            }
        }
        public override string runtimePath
        {
            get
            {
                return "Assets/CustomVFXGraph/Runtime/Shaders";
            }
        }
        public override string SRPAssetTypeStr
        {
            get
            {
                return "SteinRenderPipelineAsset";
            }
        }

        public override Type SRPOutputDataType
        {
            get
            {
                return null;
            }
        }
    }
}