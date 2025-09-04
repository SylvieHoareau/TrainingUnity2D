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
            Instantiate(item, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Le prefab 'Cherry' est introuvable.");
        }
    }
   
}
