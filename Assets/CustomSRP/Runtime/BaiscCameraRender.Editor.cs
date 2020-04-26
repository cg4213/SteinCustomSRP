using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Stein.Rendering
{
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
        ShaderTagId[] m_unsupported = {

            new ShaderTagId ("Always"),
            new ShaderTagId ("ForwardBase"),
            new ShaderTagId ("PrepassBase"),
            new ShaderTagId ("Vertex"),
            new ShaderTagId ("VertexLMRGBM"),
            new ShaderTagId ("VertexLM"),

        };

        void DrawUnsupportedShaders ()
        {
            var sortingSettings = new SortingSettings (this.camera);
            var drawingSettings = new DrawingSettings (m_unsupported[0], sortingSettings);
            drawingSettings.overrideMaterial = errorMaterial;

            for (int i = 1; i < m_unsupported.Length; ++i)
            {
                drawingSettings.SetShaderPassName (i, m_unsupported[i]);
            }

            var filteringSettings = new FilteringSettings (RenderQueueRange.all);

            context.DrawRenderers (this.cullingresult, ref drawingSettings, ref filteringSettings);
        }

        void DrawGizmos ()
        {
            if (Handles.ShouldRenderGizmos ())
            {
                context.DrawGizmos (camera, GizmoSubset.PreImageEffects);
                context.DrawGizmos (camera, GizmoSubset.PostImageEffects);
            }
        }
    }
#endif
}