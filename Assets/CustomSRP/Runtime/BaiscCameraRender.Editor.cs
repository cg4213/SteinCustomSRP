using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Stein.Rendering
{

    // public partial class BaiscCameraRender
    // {
    //     partial void DrawUnsupportedShaders ();
    // }
#if UNITY_EDITOR

    public partial class BaiscCameraRender
    {
        static Material s_errorMaterial;
        static Material errorMaterial
        {
            get
            {
                if (s_errorMaterial == null)
                    s_errorMaterial = new Material (Shader.Find ("Hidden/InternalErrorShader"));

                return s_errorMaterial;
            }
        }
        /// <summary>
        /// 所有的legacy shader pass
        /// </summary>
        /// <value></value>
        ShaderTagId[] m_unsupported = {

            new ShaderTagId ("Always"),
            new ShaderTagId ("ForwardBase"),
            new ShaderTagId ("PrepassBase"),
            new ShaderTagId ("Vertex"),
            new ShaderTagId ("VertexLMRGBM"),
            new ShaderTagId ("VertexLM"),

        };
        /// <summary>
        /// 不支持的shader pass 使用errorMaterial 渲染
        /// </summary>
        void DrawUnsupportedShaders ()
        {
            var sortingSettings = new SortingSettings (this.camera);
            var drawingSettings = new DrawingSettings (m_unsupported[0], sortingSettings);
            //替换material，好奇camera的replacement是不是同样机制
            drawingSettings.overrideMaterial = errorMaterial;

            for (int i = 1; i < m_unsupported.Length; ++i)
            {
                drawingSettings.SetShaderPassName (i, m_unsupported[i]);
            }

            var filteringSettings = new FilteringSettings (RenderQueueRange.all);

            context.DrawRenderers (this.cullingresult, ref drawingSettings, ref filteringSettings);
        }
        /// <summary>
        /// 
        /// </summary>
        void DrawGizmos ()
        {
            if (Handles.ShouldRenderGizmos ())
            {
                context.DrawGizmos (camera, GizmoSubset.PreImageEffects);
                context.DrawGizmos (camera, GizmoSubset.PostImageEffects);
            }
        }
        /// <summary>
        /// 使UI能够在SceneView出现
        /// </summary>
        void PrepareForSceneWindow ()
        {
            if (this.camera.cameraType == CameraType.SceneView)
            {
                ScriptableRenderContext.EmitWorldGeometryForSceneView (this.camera);
            }

        }

        void PrepareBuffer ()
        {

            Profiler.BeginSample ("Editor Only");
            this.m_bufferName = this.camera.name;
            Profiler.EndSample ();
            
            this.buffer.name = m_bufferName;
            this.m_sampleName = m_bufferName;
        }
    }
#endif
}