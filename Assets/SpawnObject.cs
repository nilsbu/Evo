using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject plantPrefab;

    public int plantIdx = 0;

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject p = Instantiate(plantPrefab, hit.point, Quaternion.identity);

            p.GetComponent<Lifecycle>().plant = Plant.FromPrototype(plantIdx);
            plantIdx = (plantIdx + 1) % 2;
        }
    }
}
