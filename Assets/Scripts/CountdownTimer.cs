using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float duration = 10f; // Durée total du compte à rebours
    private float currentTime; // Temps restant

    public TextMeshProUGUI timerText; // Text UI pour afficher le temps

    private bool isRunning = false;

    // Au démarrage du jeu
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        currentTime = duration;
        timerText.text = "Timer : " + duration.ToString();
        // StartCountdown();
        StartCoroutine(CountdownCoroutine());
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
        }
    }

    // --- AVEC COROUTINE ---
    IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timerText.text = "Timer : " + currentTime.ToString();
            Debug.Log("Temps restant : " + currentTime);
        }

        // ou 
        // while (true)
        // {
        //     yield return new WaitForSeconds(1f);
        //     currentTime--;
        //     timerText.text = "Timer : " + currentTime.ToString();
        //     Debug.Log("Temps restant : " + currentTime);

        //     if (currentTime <= 0)
        //     {
        //         Debug.Log("Timer terminé !");
        //         StopCoroutine(CountdownCoroutine());
        //         // break; // Sort de la boucle
        //     }
        // }
    }   

    // --- SANS COROUTINE ---
    // public void StartCountdown()
    // {
    //     currentTime = duration; // Réinitialise le temps
    //     isRunning = true;
    // }

    // private void UpdateTimerUI()
    // {
    //     if (timerText != null)
    //     {
    //         timerText.text = "Timer : " + Mathf.CeilToInt(currentTime).ToString();
    //     }
    // }
}