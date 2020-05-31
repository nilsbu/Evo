using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform parent;

    public float pitchDeg = 16f;
    public Vector3 position = new Vector3(0f, 2f, -3f);
    // Start is called before the first frame update
    void Start()
    {
        float pitchRad = pitchDeg * Mathf.Deg2Rad;
        transform.rotation = new Quaternion(
            Mathf.Sin(pitchRad / 2), 0, 0,
            Mathf.Cos(pitchRad / 2));
        transform.position = parent.position + position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position + position;
    }
}
