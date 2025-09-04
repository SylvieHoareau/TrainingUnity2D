using UnityEngine;

/// <summary>
/// Spawn: instancie des objets (cerises) selon des paramètres configurables.
/// La logique d'instanciation est extraite dans une méthode unique pour éviter la duplication.
/// </summary>
public class SpawnCherry : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Préfère assigner le prefab à instancier (ex: cherryChild)")]
    [SerializeField] private GameObject cherryChild;

    [Tooltip("Parent sous lequel placer l'objet instancié (peut être laissé vide)")]
    [SerializeField] private Transform cherry;

    [Header("Paramètres de spawn")]
    [SerializeField] private Vector3 spawnPosition = new Vector3(10f, 10f, 10f);
    [SerializeField] private Vector3 spawnEuler = new Vector3(45f, 45f, 45f);

    [Header("Contrôle")]
    [Tooltip("Instancier une fois au démarrage si vrai")]
    [SerializeField] private bool spawnOnStart = true;
    [Tooltip("Instancier à chaque Update si vrai (utiliser avec précaution)")]
    [SerializeField] private bool spawnEveryFrame = false;

    // --- Méthode utilitaire unique qui crée et retourne l'instance ---
    private GameObject CreateCherryInstance(Vector3 position, Quaternion rotation)
    {
        if (cherryChild == null)
        {
            Debug.LogWarning("Spawn: cherryChild (prefab) non assigné.");
            return null;
        }

        // Utilise l'overload d'Instantiate qui accepte le parent pour éviter un SetParent séparé
    GameObject instance = Instantiate(cherryChild, position, rotation, cherry);
        return instance;
    }

    // Au démarrage, on peut instancier une fois si demandé
    void Start()
    {
        if (spawnOnStart)
        {
            Quaternion rot = Quaternion.Euler(spawnEuler);
            CreateCherryInstance(spawnPosition, rot);
        }
    }

    // Update: si l'utilisateur a demandé un spawn chaque frame (utile uniquement pour tests), on réutilise la même méthode
    void Update()
    {
        if (spawnEveryFrame)
        {
            Quaternion rot = Quaternion.Euler(spawnEuler);
            CreateCherryInstance(spawnPosition, rot);
        }
    }
}
