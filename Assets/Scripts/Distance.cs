using UnityEngine;
using TMPro;

public class Distance : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject player;
    public GameObject enemy;

    [SerializeField] private TextMeshProUGUI distanceText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Trouver automatiquement TMP s'il n'a pas été assigné dans l'Inspector
        if (distanceText == null)
        {
            distanceText = GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        }

        if (distanceText == null)
        {
            Debug.LogWarning("Distance: TextMeshProUGUI not assigned or found. Assign it in the Inspector.");
        }

        if (player == null) Debug.LogWarning("Distance: 'player' GameObject not assigned.");
        if (enemy == null) Debug.LogWarning("Distance: 'enemy' GameObject not assigned.");

        // Optionally update initial value
        DistanceMeasure();
    }

    // Update is called once per frame
    void Update()
    {
        // Update every frame so that the text follows movements
        DistanceMeasure();
    }

    public void DistanceMeasure()
    {
        if (player == null || enemy == null)
            return;

        float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

        if (distanceText != null)
        {
            distanceText.text = "Distance: " + distance.ToString("F2"); // F2 est un format avec 2 décimales
        }
        else
        {
            Debug.Log("Distance entre le player et son enemy: " + distance.ToString("F2"));
        }
    }
}
