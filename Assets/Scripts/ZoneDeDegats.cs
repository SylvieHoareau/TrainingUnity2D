using UnityEngine;

public class ZoneDeDegats : MonoBehaviour
{
    public float damageInterval = 1f; // Temps en secondes entre chaque dégâts
    public int damageAmount = 10;// Montant de dégâts infligés

    private float timer;
    private PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant en collision est le joueur
        if (other.CompareTag("Player"))
        {
            // On récupère le script PlayerHealth du joueur
            playerHealth = other.GetComponent<PlayerHealth>();
            // On réinitialise le timer
            timer = damageInterval;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // On vérifie si l'objet qui est dans la zone est le joueur
        if (other.CompareTag("Player") && playerHealth != null)
        {
            // On décrémente le timer
            timer -= Time.deltaTime;

            // Si le timer est écoulé, on inflige des dégâts
            if (timer <= 0)
            {
                playerHealth.UpdateHealth(-damageAmount);
                playerHealth.UpdateSlider();
                Debug.Log("Le joueur prend des dégâts dans la zone.");

                // On réinitialise le timer
                timer = damageInterval;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le joueur quitte la zone, on retire la référence à son script
        if (other.CompareTag("Player"))
        {
            playerHealth = null;
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
