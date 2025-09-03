using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject parent;

    public GameObject child;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantier l'enfant sous le parent
        // GameObject newItem = Instantiate(child, new Vector3(10f, 10f, 10f), new Quaternion(10f, 10f, 10f, 10f));
        GameObject newItem = Instantiate(child, new Vector3(10f, 10f, 10f), Quaternion.Euler(new Vector3(45f, 45f, 45f)));
        newItem.transform.parent = parent.transform;
        // Alternatively, you can use SetParent
        // newItem.transform.SetParent(parent.transform);
        Quaternion.Euler(new Vector3(45f, 45f, 45f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
