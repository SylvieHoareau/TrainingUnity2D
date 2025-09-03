using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Logique pour "tuer" le joueur
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.FullKill();
                Debug.Log("KillPlayer: Player has been killed.");
                // Charger la scène de Game Over
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Debug.LogWarning("KillPlayer: Pas de composant PlayerHealth sur le Player.");
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
