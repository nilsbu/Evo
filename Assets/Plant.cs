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
            nextGaussian(pt.initialHeightMean, pt.initialHeightStdev),
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

    private static float nextGaussian(float mean, float sigma)
    {
        Random rand = new Random(); //reuse this if you are generating many
        float u1 = 1.0f - Random.Range(0f, 1f);
        float u2 = 1.0f - Random.Range(0f, 1f);
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                     Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        float randNormal = mean + sigma * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;
    }
}
