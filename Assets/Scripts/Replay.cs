using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Pour activer le bouton Rejouer dans le menu GameOver
public class Replay : MonoBehaviour
{
    private Button replayButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        replayButton = GetComponent<Button>();
        if (replayButton != null)
        {
            replayButton.onClick.AddListener(OnReplayButtonClicked);
        }
        else
        {
            Debug.LogWarning("Replay: No Button component found on this GameObject.");
        }
    }

    void OnReplayButtonClicked()
    {
        SceneManager.LoadScene("Scene1");
        Debug.Log("Scene1 reloaded");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
