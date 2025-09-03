using UnityEngine;

public class FrogCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // On vérifie si l'object avec lequel on est entré en collision 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Traversable"))
        {
            // On désactive la collision entre la grenouille et l'obstacle
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());            
           
        }
    }
}
