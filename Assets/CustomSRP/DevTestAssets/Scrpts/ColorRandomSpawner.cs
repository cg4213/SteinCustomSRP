using UnityEngine;

public class ColorRandomSpawner : RandomSpawnerBase
{
    public float raduis = 10;
    override protected void OnSpawned (GameObject newInst)
    {
        var position = Random.insideUnitSphere * raduis;
        newInst.transform.position = position;

        var color = Random.ColorHSV (0, 1, 0, 1, 0, 1, 1, 1);

        var propertiesComp = newInst.GetComponent<ExampleColorPerObjectMatProperties> ();
        propertiesComp.baseColor = color;
        propertiesComp.Apply();

    }
}