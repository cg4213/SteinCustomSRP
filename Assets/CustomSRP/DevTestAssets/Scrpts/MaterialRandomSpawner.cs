using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomSpawner : RandomSpawnerBase
{

    public float raduis = 10;
    public List<Material> materials;
    override protected void OnSpawned (GameObject newInst)
    {
        var position = Random.insideUnitSphere * raduis;
        var index = Random.Range (0, materials.Count);

        newInst.transform.position = position;
        var renderer = newInst.GetComponent<Renderer> ();
        renderer.sharedMaterial = materials[index];
    }
}
