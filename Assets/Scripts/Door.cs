using UnityEngine;

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

    public void OpenDoor()
    {
        doorAnimator.SetBool("isOpen", true);
        doorCollider.bodyType = RigidbodyType2D.Kinematic;
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
