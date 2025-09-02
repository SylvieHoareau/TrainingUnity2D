using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public int maxHealth = 50;
    public int currentHealth;
    public Slider healthBar;
    public int damage = 10;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GetComponent<Slider>();
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    public void Damage()
    {
        currentHealth -= damage;
        
    }
}
