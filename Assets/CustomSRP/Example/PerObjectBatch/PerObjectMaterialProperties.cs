using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Renderer))]
public abstract partial class PerObjectMaterialProperties : MonoBehaviour
{
    static MaterialPropertyBlock s_propertyBlock;

    Renderer m_renderer;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake ()
    {
        Apply ();
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate ()
    {
        Apply ();
    }
    
    public void Apply ()
    {
        // Debug.Log ("OnValidate");
        if (s_propertyBlock == null)
        {
            s_propertyBlock = new MaterialPropertyBlock ();
        }

        if (m_renderer == null)
        {
            m_renderer = GetComponent<Renderer> ();
        }
        PrepareBlock (s_propertyBlock);
        m_renderer.SetPropertyBlock (s_propertyBlock);
    }

    protected abstract void PrepareBlock (MaterialPropertyBlock block);
}