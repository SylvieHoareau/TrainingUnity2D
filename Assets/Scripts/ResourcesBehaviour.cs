using UnityEngine;
using System.Collections;

public class ResourcesBehaviour : MonoBehaviour
{
    // Ce qui doit apparaître au lancement du jeu
    void Start()
    {
        GameObject item = Resources.Load<GameObject>("Cherry");
        if (item != null)
        {
            Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Le prefab 'Cherry' est introuvable.");
        }
    }
   
}
