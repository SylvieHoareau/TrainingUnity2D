using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private GameObject player;
    public GameObject door;
    public Rigidbody2D doorCollider;
    public Animator doorAnimator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("Door");
        doorCollider = door.GetComponent<Rigidbody2D>();
        doorAnimator = door.GetComponent<Animator>();
    }

    public bool IsDoorOpen()
    {
        return doorAnimator.GetBool("isOpen");
    }

    // Au démarrage du jeu
    void Start()
    {
        // Initialement, la porte est fermée
        // CloseDoor();

        // Démarrer la coroutine pour temporiser l'ouverture et la fermeture de la porte
        StartCoroutine(TimerDoor());
    }

     // Update is called once per frame
    void Update()
    {
        
    }

    // Pour ouvrir la porte
    public void OpenDoor()
    {
        doorAnimator.SetBool("isOpen", true);
        doorCollider.bodyType = RigidbodyType2D.Kinematic;
    }

    // Pour fermer la porte 
    public void CloseDoor()
    {
        doorAnimator.SetBool("isOpen", false);
        doorCollider.bodyType = RigidbodyType2D.Kinematic;
    }

    // Coroutine pour temporiser la fermeture de la porte
    IEnumerator TimerDoor() {

        while (true)
        {
            yield return new WaitForSeconds(1);
            doorAnimator.SetBool("isOpen", true);

            yield return new WaitForSeconds(1);
            doorAnimator.SetBool("isOpen", false);
        }
        
        // doorCollider.bodyType = RigidbodyType2D.Static;
    }

   
}
