using UnityEngine;

public class ParallaxLoop : MonoBehaviour
{
    public Transform cameraTransform; // Caméra principale
    public float parallaxFactor = 0.5f; // Vitesse relative
    public float spriteWidth; // Largeur du sprite pour looping

    public Vector3 startPosition;

    // Au démarrage du jeu
    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        startPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = cameraTransform.position.x * parallaxFactor;
        transform.position = new Vector3(startPosition.x + deltaX, startPosition.y, startPosition.z);

        // Looping horizontal
        float offset = cameraTransform.position.x - startPosition.x;

        if (offset > spriteWidth)
        {
            startPosition.x += spriteWidth;
        }
        else if (offset < -spriteWidth)
        {
            startPosition.x -= spriteWidth;
        }
    }
}
