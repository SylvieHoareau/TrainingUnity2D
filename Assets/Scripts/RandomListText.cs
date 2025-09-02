using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RandomListText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private List<string> texts = new List<string>();
    [SerializeField] private float intervalSeconds = 5f; // Intervalle entre les changements de texte

    private void Awake()
    {
        if (textUI == null)
        {
            textUI = GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (texts == null || texts.Count == 0)
        {
            Debug.LogWarning("RandomTextSimple: pas de textes dans la liste");
            return;
        }

        ShowRandom();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowRandom()
    {
        if (textUI == null) return;
        int index = Random.Range(0, texts.Count);
        textUI.text = texts[index];
    }
}
