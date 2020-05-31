using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    private Material material;

    private Color color = Color.HSVToRGB(127f / 360f, .89f, .73f);
    public float initialHeightMean = .5f, initialHeightStdev = .2f;
    public float minHeight = .1f, maxHeight = 50f;
    public float radius = .1f;
    public float growthRate = .1f;

    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material;
        Debug.Log(color);
        material.color = color;


        float height = Mathf.Clamp(
            nextGaussian(initialHeightMean, initialHeightStdev),
            minHeight, maxHeight);
        transform.localScale = new Vector3(radius, height, radius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material.color = color;

        setHeightClamped(transform.localScale.y + growthRate * Time.deltaTime);
    }

    private void setHeightClamped(float height)
    {
        Vector3 scale = transform.localScale;
        Debug.Log($"{scale.y} -> {Mathf.Clamp(height, minHeight, maxHeight)} @ {growthRate}");
        scale.y = Mathf.Clamp(height, minHeight, maxHeight);
        transform.localScale = scale;
    }

    private float nextGaussian(float mean, float sigma)
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
