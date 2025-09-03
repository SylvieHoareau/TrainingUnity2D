using System;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float duration = 10f; // Durée total du compte à rebours
    private float currentTime; // Temps restant

    public TextMeshProUGUI timerText; // Text UI pour afficher le temps

    private bool isRunning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime; // Diminue en fonction du temps réel
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                Debug.Log("Timer terminé !");
            }

            UpdateTimerUI();
        }
    }

    public void StartCountdown()
    {
        currentTime = duration; // Réinitialise le temps
        isRunning = true;

    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Temps: " + Mathf.CeilToInt(currentTime).ToString();
        }
    }
}