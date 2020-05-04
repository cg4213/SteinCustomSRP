using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int num;
    public List<GameObject> spawned = new List<GameObject> ();
    [Button]
    void Spawn ()
    {
        for (int i = 0; i < num; ++i)
        {
            var newInst = GameObject.Instantiate (prefab);

            spawned.Add (newInst);
            newInst.transform.SetParent (transform);
            OnSpawned (newInst);
        }
    }

    [Button]
    void Clear ()
    {

        foreach (var item in spawned)
        {
            if (item == null)
                continue;
            if (Application.isPlaying)
            {
                Object.Destroy (item);
            }
            else
            {
                Object.DestroyImmediate (item);
            }
        }

        spawned.Clear ();
    }

    public float raduis = 10;
    public List<Material> materials;
    void OnSpawned (GameObject newInst)
    {
        var position = Random.insideUnitSphere * raduis;
        var index = Random.Range (0, materials.Count);

        newInst.transform.position = position;
        var renderer = newInst.GetComponent<Renderer> ();
        renderer.sharedMaterial = materials[index];
    }
}