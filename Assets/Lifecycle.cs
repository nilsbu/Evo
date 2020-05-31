using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    private Material material;

    public Plant plant;

    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Renderer>().material;

        material.color = plant.color;

        transform.localScale = new Vector3(plant.radius, plant.height, plant.radius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        setHeightClamped(transform.localScale.y + plant.growthRate * Time.deltaTime);
    }

    private void setHeightClamped(float height)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Clamp(height, plant.minHeight, plant.maxHeight);
        transform.localScale = scale;
    }
}
