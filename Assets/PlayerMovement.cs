using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    public float movementSpeed = 1000f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-movementSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("w"))
        {
            rb.AddForce(1, 0, movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(1, 0, -movementSpeed * Time.deltaTime);
        }
        Vector3 pos = transform.position;
        pos.y = .5f + Terrain.activeTerrain.SampleHeight(transform.position);
        transform.position = pos;
    }
}
