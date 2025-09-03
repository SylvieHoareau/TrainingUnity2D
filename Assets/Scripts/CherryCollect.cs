using TMPro;
using UnityEngine;

public class CherryCollect : MonoBehaviour
{
    public TextMeshProUGUI cherryText; // Le textUI pour afficher le nombre de cerises collectées

    private int cherryCount = 0;
    // Au démarrage du jeu
    void Start()
    {
        // Le nombre de cerises collectées est à Zéro au départ
        UpdateCherryUI();
        Debug.Log("Nombre de cerises collectées: " + cherryCount);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cherry"))
        {
            cherryCount++;
            Debug.Log("Cerises collectées: " + cherryCount);

            Destroy(other.gameObject); // détruit la cerise ramassée
            UpdateCherryUI();
        }
    }

    private void UpdateCherryUI()
    {
        if (cherryText != null)
        {
            cherryText.text = "Cerises: " + cherryCount.ToString();
        }
        else
        {
            Debug.LogWarning("CherryCollect: No TextMeshProUGUI assigned for cherryText.");
        }
    }
}
