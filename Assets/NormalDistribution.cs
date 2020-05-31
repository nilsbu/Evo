using UnityEngine;

public class NormalDistribution
{
    public static float Rand(float mu, float sigma)
    {
        float u1 = 1.0f - Random.Range(0f, 1f);
        float u2 = 1.0f - Random.Range(0f, 1f);
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                     Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        float randNormal = mu + sigma * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;
    }
}
