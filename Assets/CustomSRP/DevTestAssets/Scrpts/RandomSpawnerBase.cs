using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class RandomSpawnerBase : MonoBehaviour
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
    abstract protected void OnSpawned (GameObject newInst);
}
