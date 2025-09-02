using UnityEngine;
using System.Collections; // Pour IEnumerator
using TMPro;

public class CoroutineWaitText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coroutineText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If not assigned in Inspector, try to find one on the GameObject or its children
        if (coroutineText == null)
        {
            coroutineText = GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        }

        if (coroutineText == null)
        {
            // Debug.LogError("CoroutineWaitText: no TextMeshProUGUI assigned or found on the GameObject. Coroutine aborted.");
            return;
        }

        StartCoroutine(WaitForText());
    }

    public IEnumerator WaitForText()
    {
        // Guard: coroutineText should be set by Start()
        coroutineText.text = "Loading...";
        // Debug.Log("Coroutine started, waiting for 2 seconds...");

        yield return new WaitForSeconds(2f);

        coroutineText.text = "Ready! From Coroutine.";
        // Debug.Log("Coroutine finished, text updated.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
