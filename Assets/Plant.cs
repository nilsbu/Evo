using UnityEngine;
using System.IO;

[System.Serializable]
struct PlantPrototypes
{
    public PlantPrototype[] prototypes;
}

[System.Serializable]
struct PlantPrototype
{
    public Color color;
    public float initialHeightMean, initialHeightStdev;
    public float minHeight, maxHeight;
    public float radius;
    public float growthRate;
}

public class Plant
{
    private static PlantPrototypes prototypes = ReadPrototypes();

    public Color color;
    public float height;
    public float minHeight, maxHeight;
    public float radius;
    public float growthRate;

    private Plant()
    {
    }

    public static Plant FromPrototype(int prototypeIdx)
    {
        Plant plant = new Plant();
        PlantPrototype pt = prototypes.prototypes[prototypeIdx];
        plant.color = pt.color;
        plant.height = Mathf.Clamp(
            NormalDistribution.Rand(pt.initialHeightMean, pt.initialHeightStdev),
            pt.minHeight, pt.maxHeight);
        plant.minHeight = pt.minHeight;
        plant.maxHeight = pt.maxHeight;
        plant.radius = pt.radius;
        plant.growthRate = pt.growthRate;

        return plant;
    }

    private static PlantPrototypes ReadPrototypes()
    {
        string path = "Assets/PlantPrototypes.json";
        StreamReader reader = new StreamReader(path);
        string str = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<PlantPrototypes>(str);
    }
}
