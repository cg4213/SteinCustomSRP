using UnityEngine;

public class ExampleColorPerObjectMatProperties : PerObjectMaterialProperties
{
    public Color baseColor;

    protected override void PrepareBlock (MaterialPropertyBlock block)
    {
        block.SetColor (_BaseColorID, baseColor);
    }

    // void OnValidate ()
    // {
    //     Debug.Log ("OnValidate");
    // }
}