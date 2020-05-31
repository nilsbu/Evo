using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject plantPrefab;

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(plantPrefab, hit.point, Quaternion.identity);
        }
    }
}
