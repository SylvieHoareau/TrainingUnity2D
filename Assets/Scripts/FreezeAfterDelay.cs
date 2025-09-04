using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Activer la gravité
        rb.gravityScale = 1f;

        // Lancer la coroutine pour geler la position après un délai
        StartCoroutine(FreezeAfterSeconds(2f));
    }

    // Update is called once per frame
    private System.Collections.IEnumerator FreezeAfterSeconds(float seconds)
    {
        // Attendre le délai spécifié
        yield return new WaitForSeconds(seconds);

        // Geler la position en désactivant la gravité et en mettant la vitesse à zéro
        // Gère la position sur X et Y + rotation
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        // Désactiver la gravité
        rb.gravityScale = 0f;
    }
}
