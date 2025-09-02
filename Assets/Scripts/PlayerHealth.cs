using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;
    public Slider healthBar;

    public bool death = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        FullHeal();
    }

    void Update()
    {
        // Ancien input Sytem, à remplacer par le nouveau Input System plus tard
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     UpdateHealth(-20);
        //     UpdateSlider();
        //     // ou 
        //     // HealthToChange = -20; 
        //     // UpdateHealth(HealthToChange);
        //     // UpdateSlider();
        //     Debug.Log("Player took damage");
        // }

        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     UpdateHealth(20);
        //     UpdateSlider();
        //     // ou
        //     // HealthToChange = 20; 
        //     // UpdateHealth(HealthToChange);
        //     // UpdateSlider();
        //     Debug.Log("Player healed");
        // }
    }

    // Appuyer sur la touche K pour infliger des dégâts
    public void TakeDamage(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UpdateHealth(-20);
            UpdateSlider();
            Debug.Log("Player took damage");
        }
    }

    // Appuyer sur la touche V pour soigner
    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UpdateHealth(20);
            UpdateSlider();
            Debug.Log("Player healed");
        }
    }

    // Méthodes compatibles avec PlayerInput (Behavior = Send Messages)
    // PlayerInput enverra OnDamage / OnHeal avec un InputValue lorsque les actions sont déclenchées
    public void OnDamage(InputValue value)
    {
        if (value == null) return;
        if (value.Get<float>() > 0.5f)
        {
            UpdateHealth(-20);
            UpdateSlider();
            Debug.Log("Player took damage (OnDamage)");
        }
    }

    public void OnHeal(InputValue value)
    {
        if (value == null) return;
        if (value.Get<float>() > 0.5f)
        {
            UpdateHealth(20);
            UpdateSlider();
            Debug.Log("Player healed (OnHeal)");
        }
    }

    // Fonction pour infliger des dégâts au joueur
    // public void TakeDamage(int damage)
    // {
    //     currentHealth -= damage;
    //     currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Pour donner des limites à la barre de vie
    //     healthBar.value = currentHealth;
    // }

    // Fonction pour soigner le joueur
    // public void Heal(int healAmount)
    // {
    //     currentHealth += healAmount;
    //     currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Pour donner des limites à la barre de vie
    //     healthBar.value = currentHealth;
    // }

    // Fonction pour mettre à jour la santé du joueur
    public void UpdateHealth(int HealthToChange)
    {
        currentHealth += HealthToChange;
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < minHealth)
        {
            currentHealth = minHealth;
            death = true;
        }
    }

    // Fonction pour mettre à jour la barre de vie
    public void UpdateSlider()
    {
        healthBar.value = currentHealth;
        Debug.Log("Health Bar Updated: " + healthBar.value);
    }

    // Fonction pour remettre la vie du joueur à son maximum
    // Utile pour les checkpoints ou le respawn
    public void FullHeal()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        death = false;
    }
}
