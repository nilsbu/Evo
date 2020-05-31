using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    public float speed = 6f;
    public float shiftSpeed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveDirection *= shiftSpeed;
        }
        else
        {
            moveDirection *= speed;
        }
        characterController.Move(moveDirection * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x,
            Terrain.activeTerrain.transform.position.x + transform.lossyScale.x / 2,
            Terrain.activeTerrain.transform.position.x + Terrain.activeTerrain.terrainData.size.x - transform.lossyScale.x / 2);
        pos.z = Mathf.Clamp(pos.z,
            Terrain.activeTerrain.transform.position.z + transform.lossyScale.x / 2,
            Terrain.activeTerrain.transform.position.z + Terrain.activeTerrain.terrainData.size.z - transform.lossyScale.x / 2);
        pos.y = .5f + Mathf.Max(0, Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.transform.position.y);
        
        transform.position = pos;
    }
}
